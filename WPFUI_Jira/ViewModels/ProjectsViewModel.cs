using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Ninject;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores.Interfaces;
using WPFUI_Jira.Views;

namespace WPFUI_Jira.ViewModels;
public partial class ProjectsViewModel : BaseViewModel
{
	public IAuthenticationService AuthenticationService { get; init; }

	private StandardKernel _kernel;

	private IUserService _userService;

	private IProjectService _projectService;

	private User _user;

	[ObservableProperty]
	private IEnumerable<Project> _projects;

    public ProjectsViewModel(IAuthenticationService authenticationService, INavigationService navigationService) : base(navigationService)
    {
		AuthenticationService = authenticationService;

		_userService = App.GetService<IUserService>();
		_projectService = App.GetService<IProjectService>();

		_user = AuthenticationService.AccountStore.CurrentAccount;
		Projects = _projectService.GetProjects(_user.Id);
	}

	[RelayCommand]
	public void ViewProject(Project project)
	{
		var projStore = App.AppHost.Services.GetRequiredService<IProjectStore>();
		projStore.CurrentProject = project;

		_navigationService.Navigate(typeof(TaskBoardView));
	}
}
