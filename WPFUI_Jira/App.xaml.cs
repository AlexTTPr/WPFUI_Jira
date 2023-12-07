// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Threading;
using WPFUI_Jira.Models.Repository;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models.Services;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores;
using WPFUI_Jira.Models.Stores.Interfaces;
using WPFUI_Jira.Services;
using WPFUI_Jira.ViewModels;
using WPFUI_Jira.ViewModels.Pages;
using WPFUI_Jira.ViewModels.Windows;
using WPFUI_Jira.Views;
using WPFUI_Jira.Views.Pages;
using WPFUI_Jira.Views.Windows;

namespace WPFUI_Jira;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
	// The.NET Generic Host provides dependency injection, configuration, logging, and other services.
	// https://docs.microsoft.com/dotnet/core/extensions/generic-host
	// https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
	// https://docs.microsoft.com/dotnet/core/extensions/configuration
	// https://docs.microsoft.com/dotnet/core/extensions/logging
	public static readonly IHost AppHost = Host
		.CreateDefaultBuilder()
		.ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
		.ConfigureServices((context, services) =>
		{
			services.AddHostedService<ApplicationHostService>();

			services.AddSingleton<MainWindow>();
			services.AddSingleton<MainWindowViewModel>();

			services.AddSingleton<INavigationService, NavigationService>();
			services.AddSingleton<ISnackbarService, SnackbarService>();
			services.AddSingleton<IContentDialogService, ContentDialogService>();
			services.AddSingleton<IAuthenticationService, AuthenticationService>();
			services.AddSingleton<IAccountStore, AccountStore>();
			services.AddSingleton<IProjectStore, ProjectStore>();
			services.AddSingleton<IUserService, UserService>();
			services.AddSingleton<IDbRepos, DbRepos>();
			services.AddSingleton<IProjectService, ProjectService>();
			services.AddSingleton<ITaskBoardService, TaskBoardService>();
			services.AddSingleton<ITaskListService, TaskListService>();
			services.AddSingleton<ITaskCardService, TaskCardService>();
			services.AddSingleton<ITaskCardStore, TaskCardStore>();

			services.AddSingleton<ProjectsView>();
			services.AddSingleton<ProjectsViewModel>();
			services.AddSingleton<TaskBoardView>();
			services.AddSingleton<TaskBoardViewModel>();

			services.AddSingleton<DashboardPage>();
			services.AddSingleton<DashboardViewModel>();
			services.AddSingleton<DataPage>();
			services.AddSingleton<DataViewModel>();
			services.AddSingleton<SettingsPage>();
			services.AddSingleton<SettingsViewModel>();
		}).Build();

	static App()
	{
		//tmp hdcd
		var authService = AppHost.Services.GetRequiredService<IAuthenticationService>();
		authService.AccountStore.CurrentUser = AppHost.Services.GetRequiredService<IUserService>().GetUser(2);
	}

	/// <summary>
	/// Gets registered service.
	/// </summary>
	/// <typeparam name="T">Type of the service to get.</typeparam>
	/// <returns>Instance of the service or <see langword="null"/>.</returns>
	public static T? GetService<T>()
		where T : class
	{
		return AppHost.Services.GetService(typeof(T)) as T;
	}

	/// <summary>
	/// Occurs when the application is loading.
	/// </summary>
	private void OnStartup(object sender, StartupEventArgs e)
	{
		AppHost.Start();
	}

	/// <summary>
	/// Occurs when the application is closing.
	/// </summary>
	private async void OnExit(object sender, ExitEventArgs e)
	{
		await AppHost.StopAsync();

		AppHost.Dispose();
	}

	/// <summary>
	/// Occurs when an exception is thrown by an application but not handled.
	/// </summary>
	private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
	{
		// For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
	}
}
