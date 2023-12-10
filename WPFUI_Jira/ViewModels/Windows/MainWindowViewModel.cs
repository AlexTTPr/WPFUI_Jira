// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using WPFUI_Jira.Models;
using WPFUI_Jira.Views;

namespace WPFUI_Jira.ViewModels.Windows;
public partial class MainWindowViewModel : ObservableObject
{
	[ObservableProperty]
	private string _applicationTitle = "MyJira";

	[ObservableProperty]
	private ObservableCollection<object> _menuItems = new()
		{
			new NavigationViewItem()
			{
				Content = "Проекты",
				Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
				TargetPageType = typeof(ProjectsView)
			}
		};

	[ObservableProperty]
	private ObservableCollection<object> _footerMenuItems = new()
		{
			new NavigationViewItem()
			{
				Content = "Настройки",
				Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
				TargetPageType = typeof(Views.Pages.SettingsPage)
			}
		};
}
