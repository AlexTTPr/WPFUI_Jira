using GongSolutions.Wpf.DragDrop;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Ninject;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores.Interfaces;
using WPFUI_Jira.Views;

namespace WPFUI_Jira.ViewModels;

public partial class TaskBoardViewModel : BaseViewModel, IDropTarget
{
	public IAuthenticationService AuthenticationService { get; init; }
	public IContentDialogService ContentDialogService { get; init; }

	private IProjectStore _projectStore;
	private IProjectService _projectService;
	private ITaskBoardService _taskBoardService;
	private ITaskListService _taskListService;
	private ITaskCardService _taskCardService;
	private IUserService _userService;

	[ObservableProperty]
	private Project _project;

	[ObservableProperty]
	private bool _isOwner;

	[ObservableProperty]
	private TaskBoard _taskBoard;

	[ObservableProperty]
	private TaskCard? _chosenTaskCard;

	[ObservableProperty]
	private bool _isTaskCardFlyoutOpen;

	[ObservableProperty]
	private bool _isTaskListCreateFlyoutOpen;

	[ObservableProperty]
	private bool _isTaskListRedactFlyoutOpen;

	[ObservableProperty]
	private ListType _currentListType;

	[ObservableProperty]
	private string? _currentTaskListTitle;

	[ObservableProperty]
	private TaskList? _currentTaskList;

	public TaskBoardViewModel(IAuthenticationService authenticationService, IContentDialogService contentDialogService, IProjectStore projectStore, INavigationService navigationService) : base(navigationService)
	{
		_projectStore = projectStore;
		AuthenticationService = authenticationService;
		ContentDialogService = contentDialogService;

		_projectService = App.GetService<IProjectService>();
		_taskBoardService = App.GetService<ITaskBoardService>();
		_taskCardService = App.GetService<ITaskCardService>();
		_taskListService = App.GetService<ITaskListService>();
		_userService = App.GetService<IUserService>();

		Project = _projectStore.CurrentProject;
		//...
		Project.TaskBoard = _taskBoardService.GetTaskBoards(Project.Id).FirstOrDefault();
		Project.Workers = _userService.GetUsers(Project.Id);
		LoadTaskBoardData();

		IsOwner = Project.Owner.Id == AuthenticationService.AccountStore.CurrentAccount.Id;
		AuthenticationService.AccountStore.CurrentAccountChanged += AccountStore_CurrentAccountChanged;
	}

	private void AccountStore_CurrentAccountChanged()
	{
		IsOwner = Project.Owner.Id == AuthenticationService.AccountStore.CurrentAccount.Id;
	}

	[RelayCommand]
	public void EditTask(TaskCard taskCard)
	{
		if (!IsOwnerOrExecutor(taskCard))
			return;
		Debug.WriteLine("Edited...");
	}

	[RelayCommand]
	public async void ViewTaskDetails(object[] values)
	{
		var content = (Panel)values[0];
		var taskCard = (TaskCard)values[1];

		var taskCardStore = App.AppHost.Services.GetService<ITaskCardStore>();
		taskCardStore.CurrentTaskCard = taskCard;

		var context = App.AppHost.Services.GetService<TaskCardDetailsViewModel>();

		content.DataContext = context;

		var dialogOptions = new SimpleContentDialogCreateOptions()
		{
			Title = string.Empty,
			Content = content,
			CloseButtonText = "Закрыть"
		};

		if (IsOwner)
		{
			dialogOptions.PrimaryButtonText = "Сохранить Изменения";
			dialogOptions.SecondaryButtonText = "Удалить";
		}
		var result = await ContentDialogService.ShowSimpleDialogAsync(dialogOptions);

		switch (result)
		{
			case Wpf.Ui.Controls.ContentDialogResult.Primary:
				context.SaveChanges();
				LoadTaskBoardData();
				LoadTaskBoardData();
				break;
			case Wpf.Ui.Controls.ContentDialogResult.Secondary:
				context.Delete();
				LoadTaskBoardData();
				LoadTaskBoardData();
				break;
			case Wpf.Ui.Controls.ContentDialogResult.None:
				break;
		}
	}

	[RelayCommand]
	public void TimeReport(DateTime timeSpent)
	{
		var time = timeSpent.AddSeconds(-timeSpent.Second).TimeOfDay;
		ChosenTaskCard.Actions.Add(new(time, AuthenticationService.AccountStore.CurrentAccount.Id));
		_taskCardService.UpdateTaskCard(ChosenTaskCard);
	}

	[RelayCommand]
	public async void CreateTask(object[] values)
	{
		var content = (Panel)values[0];
		var taskList = (TaskList)values[1];

		var taskListStore = App.AppHost.Services.GetService<ITaskListStore>();
		taskListStore.CurrentTaskList = taskList;

		var context = App.AppHost.Services.GetService<CreateTaskCardViewModel>();

		content.DataContext = context;

		var dialogOptions = new SimpleContentDialogCreateOptions()
		{
			Title = string.Empty,
			Content = content,
			CloseButtonText = "Закрыть",
			PrimaryButtonText = "Сохранить"
		};

		var result = await ContentDialogService.ShowSimpleDialogAsync(dialogOptions);

		switch (result)
		{
			case Wpf.Ui.Controls.ContentDialogResult.Primary:
				context.Save();
				LoadTaskBoardData();
				LoadTaskBoardData();
				break;
			case Wpf.Ui.Controls.ContentDialogResult.Secondary:

				break;
			case Wpf.Ui.Controls.ContentDialogResult.None:
				break;
		}
	}

	void LoadTaskBoardData()
	{
		TaskBoard = null;
		TaskBoard = Project.TaskBoard;

		Project.TaskBoard.TaskLists = null;
		Project.TaskBoard.TaskLists = new ObservableCollection<TaskList>(_taskListService.GetTaskLists(Project.TaskBoard.Id));

		foreach (var taskList in Project.TaskBoard.TaskLists)
			taskList.TaskCards = new ObservableCollection<TaskCard>(_taskCardService.GetTaskCards(taskList.Id));
	}

	bool IsOwnerOrExecutor(TaskCard taskCard)
	{
		return IsOwner || taskCard.Executor?.Id == AuthenticationService.AccountStore.CurrentAccount.Id;
	}

	[RelayCommand]
	public void SetChosenTaskCard(TaskCard taskCard)
	{
		if (taskCard.Executor?.Id != AuthenticationService.AccountStore.CurrentAccount.Id)
			return;

		if (!IsTaskCardFlyoutOpen)
			IsTaskCardFlyoutOpen = true;

		ChosenTaskCard = taskCard;
	}

	[RelayCommand]
	public void OpenTaskListAdditionFlyout()
	{
		CurrentTaskList = null;
		CurrentTaskListTitle = null;
		CurrentListType = ListType.Query;
		if (!IsTaskListCreateFlyoutOpen)
			IsTaskListCreateFlyoutOpen = true;
	}

	[RelayCommand]
	public void OpenTaskListRedactingFlyout(TaskList taskList)
	{
		CurrentTaskList = taskList;
		CurrentTaskListTitle = taskList.Title;
		CurrentListType = taskList.Type;
		if (!IsTaskListRedactFlyoutOpen)
			IsTaskListRedactFlyoutOpen = true;
	}

	[RelayCommand]
	public void DeleteTaskList()
	{
		_taskListService.DeleteTaskList(CurrentTaskList.Id);
		IsTaskListRedactFlyoutOpen = false;
		LoadTaskBoardData();
		LoadTaskBoardData();
	}

	[RelayCommand]
	public void UpdateTaskList()
	{
		CurrentTaskList.Title = CurrentTaskListTitle;
		CurrentTaskList.Type = CurrentListType;
		_taskListService.UpdateTaskList(CurrentTaskList);
		LoadTaskBoardData();
		LoadTaskBoardData();
	}

	[RelayCommand]
	public void AddTaskList()
	{
		var taskList = new TaskList(CurrentListType) { Title = CurrentTaskListTitle, TaskBoardId = Project.TaskBoard.Id };

		_taskListService.CreateTaskList(taskList);
		LoadTaskBoardData();
		LoadTaskBoardData();
	}

	[RelayCommand]
	public void ViewProjectDetails()
	{
		_navigationService.Navigate(typeof(ProjectDetailsView));
	}


	#region DragAndDrop
	public void DragOver(IDropInfo dropInfo)
	{
		if (dropInfo.Data is TaskList dropList)
		{
			if (dropInfo.TargetItem is TaskList targetList && targetList != dropList)
			{
				dropInfo.Effects = DragDropEffects.Move;
				dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
			}
		}

		if (dropInfo.Data is TaskCard dropCard)
		{
			if (dropCard.Executor?.Id == AuthenticationService.AccountStore.CurrentAccount.Id || IsOwner)
			{
				if (dropInfo.TargetItem is TaskList)
				{
					dropInfo.Effects = DragDropEffects.Move;
					dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
				}
				else
				{
					dropInfo.Effects = DragDropEffects.Move;
					dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
				}
			}
			else
			{
				dropInfo.Effects = DragDropEffects.None;
			}
		}

		Debug.WriteLine(dropInfo.TargetItem?.ToString());
	}

	void IDropTarget.Drop(IDropInfo dropInfo)
	{
		if (dropInfo.TargetItem == null || dropInfo.TargetCollection == null)
			return;

		var sourceCollection = (IList)dropInfo.DragInfo.SourceCollection;

		if (dropInfo.Data is TaskCard taskCard)
		{
			if (dropInfo.TargetItem is TaskList taskList)
			{
				sourceCollection.RemoveAt(dropInfo.DragInfo.SourceIndex);
				taskList.TaskCards.Add(taskCard);

				taskCard.TaskListId = taskList.Id;
			}

			var destCollection = (IList)dropInfo.TargetCollection;

			if (destCollection != null)
			{
				if (dropInfo.TargetItem.GetType().Equals(dropInfo.Data.GetType()))
				{
					if (destCollection == sourceCollection && dropInfo.DragInfo.SourceIndex < dropInfo.InsertIndex)
					{
						sourceCollection.RemoveAt(dropInfo.DragInfo.SourceIndex);
						destCollection.Insert(dropInfo.InsertIndex - 1, dropInfo.Data);
					}
					else
					{
						sourceCollection.RemoveAt(dropInfo.DragInfo.SourceIndex);
						destCollection.Insert(dropInfo.InsertIndex, dropInfo.Data);
					}

					//i should be burning in hell for this
					taskCard.TaskListId = ((TaskCard)destCollection[0]).TaskListId;
				}
			}
			_taskCardService.UpdateTaskCard(taskCard);
		}

		if (dropInfo.Data is TaskList)
		{
			//fuck...
			(sourceCollection[sourceCollection.IndexOf((TaskList)dropInfo.TargetItem)], sourceCollection[sourceCollection.IndexOf((TaskList)dropInfo.Data)]) = (sourceCollection[sourceCollection.IndexOf((TaskList)dropInfo.Data)], sourceCollection[sourceCollection.IndexOf((TaskList)dropInfo.TargetItem)]);
		}
	}
	#endregion
}