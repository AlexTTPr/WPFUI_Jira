using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using GongSolutions.Wpf.DragDrop.Utilities;
using WPFUI_Jira.Models;
using System;
using System.Windows.Input;
using System.Collections;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WPFUI_Jira.Models.Repository;
using Ninject;
using WPFUI_Jira.Models.Ninject;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores.Interfaces;
using WPFUI_Jira.Models.Stores;
using WPFUI_Jira.Models.Services;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace WPFUI_Jira.ViewModels;

public partial class TaskBoardViewModel : BaseViewModel, IDropTarget
{
	public IAuthenticationService AuthenticationService { get; init; }
	public IContentDialogService ContentDialogService {  get; init; }

	private StandardKernel _kernel;

	private IProjectStore _projectStore;
    private IProjectService _projectService;
	private ITaskBoardService _taskBoardService;
    private ITaskListService _taskListService;
    private ITaskCardService _taskCardService;
	private IUserService _userService;

	[ObservableProperty]
	private Project _project;

	public bool IsOwner => Project.Owner.Id == AuthenticationService.AccountStore.CurrentUser.Id;

	[ObservableProperty]
	private TaskBoard _taskBoard;

	[ObservableProperty]
	private TaskCard? _chosenTaskCard;

	[ObservableProperty]
	private bool _isTaskCardFlyoutOpen;

	public TaskBoardViewModel(IAuthenticationService authenticationService, IContentDialogService contentDialogService, IProjectStore projectStore, INavigationService navigationService) : base(navigationService)
	{
		_projectStore = projectStore;
		AuthenticationService = authenticationService;
		ContentDialogService = contentDialogService;

        _kernel = new StandardKernel(new NinjectRegistration());

		_projectService = _kernel.Get<IProjectService>();
		_taskBoardService = _kernel.Get<ITaskBoardService>();
		_taskCardService = _kernel.Get<ITaskCardService>();
        _taskListService = _kernel.Get<ITaskListService>();
		_userService = _kernel.Get<IUserService>();

		Project = _projectStore.CurrentProject;
		//...
		Project.TaskBoard = _taskBoardService.GetTaskBoards(Project.Id).First();
		Project.Workers = _userService.GetUsers(Project.Id);
		LoadTaskBoardData();
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
				context.SaveChanges();
				LoadTaskBoardData();
				context.SaveChanges();
				break;
			case Wpf.Ui.Controls.ContentDialogResult.Secondary:
				context.Delete();
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
		ChosenTaskCard.Actions.Add(new(time, AuthenticationService.AccountStore.CurrentUser.Id));
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

		TaskBoard.TaskLists = new ObservableCollection<TaskList>(_taskListService.GetTaskLists(TaskBoard.Id));

		foreach (var taskList in TaskBoard.TaskLists)
			taskList.TaskCards = new ObservableCollection<TaskCard>(_taskCardService.GetTaskCards(taskList.Id));
	}

	bool IsOwnerOrExecutor(TaskCard taskCard)
	{
		return IsOwner || taskCard.Executor?.Id == AuthenticationService.AccountStore.CurrentUser.Id;
	}

	[RelayCommand]
	public void SetChosenTaskCard(TaskCard taskCard)
	{
		if (taskCard.Executor?.Id != AuthenticationService.AccountStore.CurrentUser.Id)
			return;

		if (!IsTaskCardFlyoutOpen)
			IsTaskCardFlyoutOpen = true;

		ChosenTaskCard = taskCard;
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
            if(dropCard.Executor?.Id == AuthenticationService.AccountStore.CurrentUser.Id || IsOwner)
            {
                if(dropInfo.TargetItem is TaskList)
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
        if (dropInfo.TargetItem != null && dropInfo.TargetCollection != null)
        {
			var sourceCollection = (IList)dropInfo.DragInfo.SourceCollection;

			if (dropInfo.Data is TaskCard)
            {
				if (dropInfo.TargetItem is TaskList taskList)
				{
					sourceCollection.RemoveAt(dropInfo.DragInfo.SourceIndex);
					taskList.TaskCards.Add((TaskCard)dropInfo.Data);

					_taskCardService.UpdateTaskCard((TaskCard)dropInfo.Data);
					return;
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

						_taskCardService.UpdateTaskCard((TaskCard)dropInfo.Data);
					}
				}
			}
			
			if (dropInfo.Data is TaskList)
			{
				//fuck...
				(sourceCollection[sourceCollection.IndexOf((TaskList)dropInfo.TargetItem)], sourceCollection[sourceCollection.IndexOf((TaskList)dropInfo.Data)]) = (sourceCollection[sourceCollection.IndexOf((TaskList)dropInfo.Data)], sourceCollection[sourceCollection.IndexOf((TaskList)dropInfo.TargetItem)]);
			}
        }
    }
	#endregion
}