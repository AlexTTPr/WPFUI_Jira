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

	public bool IsOwner => _projectStore.CurrentProject.Owner.Id == _accountStore.CurrentUser.Id;

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

	public TaskCardDetailsViewModel(ITaskCardStore taskCardStore, IProjectStore projectStore, IAccountStore accountStore, ITaskCardService taskCardService,INavigationService navigationService) : base(navigationService)
	{
		_taskCardStore = taskCardStore;
		_projectStore = projectStore;
		_accountStore = accountStore;

		_taskCardService = taskCardService;

		var taskCard = _taskCardStore.CurrentTaskCard;
		Title = taskCard.Title;
		Description = taskCard.Description;
		Executor = taskCard.Executor;
		ExpirationTime = taskCard.ExpirationTime;
		CreationTime = taskCard.CreationTime;
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
