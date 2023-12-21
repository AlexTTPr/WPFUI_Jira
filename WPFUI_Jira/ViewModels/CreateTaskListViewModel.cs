using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores.Interfaces;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Services;
using WPFUI_Jira.Models.Stores;

namespace WPFUI_Jira.ViewModels;
public partial class CreateTaskCardViewModel : BaseViewModel
{
	private readonly ITaskListStore _taskListStore;
	private readonly IProjectStore _projectStore;
	private readonly IAccountStore _accountStore;

	private readonly ITaskCardService _taskCardService;

	private TaskList _taskList;

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
	private ICollection<User>? _workers;

	public CreateTaskCardViewModel(ITaskListStore taskListStore, IProjectStore projectStore, IAccountStore accountStore, ITaskCardService taskCardService, INavigationService navigationService) : base(navigationService)
	{
		_taskListStore = taskListStore;
		_projectStore = projectStore;
		_accountStore = accountStore;
		_taskCardService = taskCardService;

		_taskList = taskListStore.CurrentTaskList;
		Workers = _projectStore.CurrentProject.Workers;
	}

	[RelayCommand]
	public void Save()
	{
		var taskCard = new TaskCard(Title, Description, null, ExpirationTime, _taskList.Id);
		_taskCardService.CreateTaskCard(taskCard);
		taskCard.Executor = Executor;
		_taskCardService.UpdateTaskCard(taskCard);
	}
}
