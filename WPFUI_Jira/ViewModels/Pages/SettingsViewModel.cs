﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows.Controls;
using Wpf.Ui.Controls;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Services;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.ViewModels.Pages;
public partial class SettingsViewModel : ObservableObject, INavigationAware
{
	private bool _isInitialized = false;

	[ObservableProperty]
	private string _appVersion = String.Empty;

	[ObservableProperty]
	private Wpf.Ui.Appearance.ThemeType _currentTheme = Wpf.Ui.Appearance.ThemeType.Unknown;

	IAuthenticationService _authenticationService;
	IUserService _userService;

	[ObservableProperty]
	private User _currentUser;

	[ObservableProperty]
	private ICollection<User> _users;

	public SettingsViewModel(IAuthenticationService authenticationService, IUserService userService)
    {
		_authenticationService = authenticationService;
		_userService = userService;

		CurrentUser = _authenticationService.AccountStore.CurrentAccount;

		_users = _userService.GetUsers(1);
		_users.Add(_userService.GetUser(1));


	}

	[RelayCommand]
	public void ChangeUser()
	{
		_authenticationService.AccountStore.CurrentAccount = _currentUser;
	}

    public void OnNavigatedTo()
	{
		if (!_isInitialized)
			InitializeViewModel();
	}

	public void OnNavigatedFrom() { }

	private void InitializeViewModel()
	{
		CurrentTheme = Wpf.Ui.Appearance.Theme.GetAppTheme();
		AppVersion = $"WPFUI_Jira - {GetAssemblyVersion()}";

		_isInitialized = true;
	}

	private string GetAssemblyVersion()
	{
		return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
			?? String.Empty;
	}

	[RelayCommand]
	private async void ShowDialog(object o)
	{
		var content = (Panel)o;

		var uiMessageBox = new Wpf.Ui.Controls.MessageBox
		{
			Title = "WPF UI Message Box",
			Content = content,
			DataContext = this
		};

		var result = await uiMessageBox.ShowDialogAsync();
	}

	[RelayCommand]
	private async void GoToTargets()
	{
		var text = new Wpf.Ui.Controls.TextBlock();
		text.Text = "Данное приложение используется для управления проектами, отслеживания задач, управления задачами и управления командой разработки";
		text.TextWrapping = TextWrapping.Wrap;
		var content = new StackPanel();
		content.Children.Add(text);
		var uiMessageBox = new Wpf.Ui.Controls.MessageBox
		{
			Title = "Для чего оно?",
			Content = content,
			DataContext = this
		};

		var result = await uiMessageBox.ShowDialogAsync();
	}

	[RelayCommand]
	private void OnChangeTheme(string parameter)
	{
		switch (parameter)
		{
			case "theme_light":
				if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Light)
					break;

				Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
				CurrentTheme = Wpf.Ui.Appearance.ThemeType.Light;

				break;

			default:
				if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Dark)
					break;

				Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
				CurrentTheme = Wpf.Ui.Appearance.ThemeType.Dark;

				break;
		}
	}
}
