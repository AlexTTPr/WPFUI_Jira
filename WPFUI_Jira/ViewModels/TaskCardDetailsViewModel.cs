using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.ViewModels;

public partial class TaskCardDetailsViewModel : BaseViewModel
{
	private readonly ITaskCardStore _taskCardStore;
	private readonly IProjectStore _projectStore;
	private readonly IAccountStore _accountStore;

	private readonly ITaskCardService _taskCardService;

	private TaskCard _taskCard;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsNotOwner))]
	private bool _isOwner;

	public bool IsNotOwner => !IsOwner;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsHaveNotExecutor))]
	private bool _isHaveExecutor;

	public bool IsHaveNotExecutor => !IsHaveExecutor;

	[ObservableProperty]
	private string _title;

	[ObservableProperty]
	private string? _description;

	[ObservableProperty]
	private User? _executor;

	[ObservableProperty]
	private DateTime? _expirationTime;

	[ObservableProperty]
	private DateTime _creationTime;

	[ObservableProperty]
	private ICollection<User>? _workers;

	public TaskCardDetailsViewModel(ITaskCardStore taskCardStore, IProjectStore projectStore, IAccountStore accountStore, ITaskCardService taskCardService, INavigationService navigationService) : base(navigationService)
	{
		_taskCardStore = taskCardStore;
		_projectStore = projectStore;
		_accountStore = accountStore;

		_taskCardService = taskCardService;

		_taskCard = _taskCardStore.CurrentTaskCard;
		Title = _taskCard.Title;
		Description = _taskCard.Description;
		Executor = _taskCard.Executor;
		ExpirationTime = _taskCard.ExpirationTime;
		CreationTime = _taskCard.CreationTime;
		Workers = _projectStore.CurrentProject.Workers;

		IsHaveExecutor = _taskCard.Executor != null;
		IsOwner = _projectStore.CurrentProject.Owner.Id == _accountStore.CurrentAccount.Id;
	}

	//[RelayCommand]
	//public void DoNotHaveExecutor()
	//{
	//	IsHaveExecutor = false;
	//	Executor = null;
	//}

	//[RelayCommand]
	//public void DoHaveExecutor()
	//{
	//	IsHaveExecutor = true;
	//}

	[RelayCommand]
	public void TakeTask()
	{
		_taskCardStore.CurrentTaskCard.Executor = _accountStore.CurrentAccount;

		_taskCardService.UpdateTaskCard(_taskCardStore.CurrentTaskCard);
	}

	[RelayCommand]
	public void ChangeExecutionMode()
	{
		IsHaveExecutor = !IsHaveExecutor;
		if (IsHaveExecutor == false)
			Executor = null;
		else
			Executor = _taskCard.Executor;
	}

	[RelayCommand]
	public void SaveChanges()
	{
		_taskCardStore.CurrentTaskCard.Title = Title;
		_taskCardStore.CurrentTaskCard.Description = Description;
		_taskCardStore.CurrentTaskCard.Executor = Executor;
		_taskCardStore.CurrentTaskCard.ExpirationTime = ExpirationTime;

		_taskCardService.UpdateTaskCard(_taskCardStore.CurrentTaskCard);
	}

	[RelayCommand]
	public void Delete()
	{
		_taskCardService.DeleteTaskCard(_taskCardStore.CurrentTaskCard.Id);
	}
}
