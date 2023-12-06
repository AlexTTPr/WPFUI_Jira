using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Ninject;
using WPFUI_Jira.Models.Services.Interfaces;

namespace WPFUI_Jira.ViewModels;
public partial class CreateTaskViewModel : BaseViewModel
{
	public IAuthenticationService AuthenticationService { get; init; }

	public TaskList TaskList { get; set; }

	public Project Project { get; set; }

	private StandardKernel _kernel;

	private ITaskCardService _taskCardService;

	private IUserService _userService;

	[ObservableProperty]
	private IEnumerable<User>? _workers;

	[ObservableProperty]
	private string _title;

	[ObservableProperty]
	private string? _description;

	[ObservableProperty]
	private User? _executor;

	[ObservableProperty]
	private DateTime? _expirationTime;

	public CreateTaskViewModel(IAuthenticationService authenticationService, Project project, TaskList taskList, NavigationService navigationService) : base(navigationService)
	{
		AuthenticationService = authenticationService;
		TaskList = taskList;
		Project = project;

		_kernel = new StandardKernel(new NinjectRegistration());
		_taskCardService = _kernel.Get<ITaskCardService>();
		_userService = _kernel.Get<IUserService>();

		_workers = _userService.GetUsers(Project.Id);
	}

	[RelayCommand]
	void Cancel()
	{

	}

	[RelayCommand]
	void CreateTask()
    {
		var task = new TaskCard(Title, Description, Executor, ExpirationTime, TaskList.Id);
		_taskCardService.CreateTaskCard(task);

	}
}
