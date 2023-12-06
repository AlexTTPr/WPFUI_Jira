using System;
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
using WPFUI_Jira.ViewModels.Pages;

namespace WPFUI_Jira.Views;
/// <summary>
/// Interaction logic for ProjectsView.xaml
/// </summary>
public partial class ProjectsView : Page, INavigableView<ProjectsViewModel>
{
	public ProjectsViewModel ViewModel { get; set; }
	public ProjectsView(ProjectsViewModel viewModel)
	{
		ViewModel = viewModel;
		DataContext = this;

		InitializeComponent();
	}
}
