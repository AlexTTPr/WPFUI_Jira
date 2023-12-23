using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Stores.Interfaces;
using LiveCharts.Defaults;
using CsvHelper;
using System.Globalization;
using System.IO;

namespace WPFUI_Jira.ViewModels;
public partial class ProjectDetailsViewModel : BaseViewModel
{
	private IProjectStore _projectStore;

	[ObservableProperty]
	private Project _project;

	[ObservableProperty]
	private SeriesCollection _seriesCollection;

	public ProjectDetailsViewModel(IProjectStore projectStore, INavigationService navigationService) : base(navigationService)
	{
		_projectStore = projectStore;
		Project = _projectStore.CurrentProject;

		SeriesCollection = new SeriesCollection();

		SeriesCollection.Add(new PieSeries
		{
			Title = "Задачи, находящиеся в очереди",
			Values = new ChartValues<ObservableValue> { new ObservableValue(Project.TaskBoard.TaskLists.Where(i=>i.Type == ListType.Query).Select(i => i.TaskCards.Count).Sum()) },
			DataLabels = true
		});

		SeriesCollection.Add(new PieSeries
		{
			Title = "Задачи, находящиеся в работе",
			Values = new ChartValues<ObservableValue> { new ObservableValue(Project.TaskBoard.TaskLists.Where(i => i.Type == ListType.InWork).Select(i => i.TaskCards.Count).Sum()) },
			DataLabels = true
		});

		SeriesCollection.Add(new PieSeries
		{
			Title = "Выполненные задачи",
			Values = new ChartValues<ObservableValue> { new ObservableValue(Project.TaskBoard.TaskLists.Where(i => i.Type == ListType.Done).Select(i => i.TaskCards.Count).Sum()) },
			DataLabels = true
		});

		GetStatistics();
	}


	[RelayCommand]
	public void GetStatistics()
	{
		//using (var writer = new StreamWriter(@"C:\Users\Public\Documents\report.csv"))

		//using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
		//{
		//	foreach(var taskList in Project.TaskBoard.TaskLists)
		//		csv.WriteRecords(taskList.TaskCards);
		//}
	}

	[RelayCommand]
	public void ViewWorkersStatistic(User user)
	{
		//_navigationService.Navigate(typeof(UserStatisticsView));
	}
}
