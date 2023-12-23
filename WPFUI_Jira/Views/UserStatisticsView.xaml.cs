﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using WPFUI_Jira.ViewModels;

namespace WPFUI_Jira.Views;
/// <summary>
/// Interaction logic for UserStatisticsView.xaml
/// </summary>
public partial class UserStatisticsView : Page, INavigableView<UserStatisticsViewModel>
{
	public UserStatisticsViewModel ViewModel { get; set; }

	public UserStatisticsView(UserStatisticsViewModel viewModel)
	{
		ViewModel = viewModel;
		DataContext = this;

		InitializeComponent();
	}
}
