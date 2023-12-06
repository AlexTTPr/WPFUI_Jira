using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using GongSolutions.Wpf.DragDrop.Utilities;
using WPFUI_Jira.Models;
using WPFUI_Jira.ViewModels;
using Wpf.Ui.Controls;

namespace WPFUI_Jira.Views;
/// <summary>
/// Interaction logic for TaskBoardView.xaml
/// </summary>
public partial class TaskBoardView : Page, INavigableView<TaskBoardViewModel>
{
	public TaskBoardViewModel ViewModel { get; set; }

	public TaskBoardView(TaskBoardViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
		InitializeComponent();
	}

    private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        ScrollViewer scrollViewer = (ScrollViewer)sender;
        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
    }

    private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
    {
        var b = (UIElement)sender;
        b.Opacity = 1;
    }

    private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
    {
        var b = (UIElement)sender;
        b.Opacity = 0.1;
    }

}
