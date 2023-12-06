using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ninject;
using System;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Ninject;
using WPFUI_Jira.Models.Services.Interfaces;

namespace WPFUI_Jira.ViewModels;

public partial class EditTaskViewModel : BaseViewModel
{
	public IAuthenticationService AuthenticationService { get; set; }

	public TaskCard TaskCard { get; set; }

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

	[ObservableProperty]
	private DateTime? _creationTime;

	public EditTaskViewModel(IAuthenticationService authenticationService, Project project, TaskCard taskCard, NavigationService navigationService) : base(navigationService)
	{
		TaskCard = taskCard;
		Project = project;

		_kernel = new StandardKernel(new NinjectRegistration());
		_taskCardService = _kernel.Get<ITaskCardService>();
		_userService = _kernel.Get<IUserService>();

		_workers = _userService.GetUsers(Project.Id);

		Title = taskCard.Title;
		Description = taskCard.Description;
		Executor = taskCard.Executor;
		ExpirationTime = taskCard.ExpirationTime;
		CreationTime = taskCard.CreationTime;
	}

	[RelayCommand]
	void DeleteTask()
	{
		_taskCardService.DeleteTaskCard(TaskCard.Id);
	}

	[RelayCommand]
	void Cancel()
	{

	}

	[RelayCommand]
	void EditTask()
	{
		TaskCard.Title = Title;
		TaskCard.Description = Description;
		TaskCard.Executor = Executor;
		TaskCard.ExpirationTime = ExpirationTime;

		_taskCardService.UpdateTaskCard(TaskCard);


	}
}