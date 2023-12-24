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
using WPFUI_Jira.Views;
using CsvHelper.Configuration;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Shapes;

namespace WPFUI_Jira.ViewModels;
public partial class ProjectDetailsViewModel : BaseViewModel
{
	private IProjectStore _projectStore;

	[ObservableProperty]
	private Project _project;

	[ObservableProperty]
	private SeriesCollection _seriesCollection;

	[ObservableProperty]
	private string _fileToSaveName;

	[ObservableProperty]
	private string _savedFileNotice;

	public ProjectDetailsViewModel(IProjectStore projectStore, INavigationService navigationService) : base(navigationService)
	{
		_projectStore = projectStore;
		Project = _projectStore.CurrentProject;

		SeriesCollection = new SeriesCollection
		{
			new PieSeries
			{
				Title = "Задачи в очереди",
				Values = new ChartValues<ObservableValue> { new ObservableValue(Project.TaskBoard.TaskLists.Where(i => i.Type == ListType.Query).Select(i => i.TaskCards.Count).Sum()) },
				DataLabels = true
			},
			new PieSeries
			{
				Title = "Задачи в работе",
				Values = new ChartValues<ObservableValue> { new ObservableValue(Project.TaskBoard.TaskLists.Where(i => i.Type == ListType.InWork).Select(i => i.TaskCards.Count).Sum()) },
				DataLabels = true
			},
			new PieSeries
			{
				Title = "Выполненные задачи",
				Values = new ChartValues<ObservableValue> { new ObservableValue(Project.TaskBoard.TaskLists.Where(i => i.Type == ListType.Done).Select(i => i.TaskCards.Count).Sum()) },
				DataLabels = true
			}
		};
	}

	[RelayCommand]
	public async Task SaveFile(CancellationToken cancellation)
	{
		SaveFileDialog saveFileDialog =
			new()
			{
				Filter = "Text Files (*.csv)|*.csv",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};

		if (!String.IsNullOrEmpty(FileToSaveName))
		{
			var invalidChars = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());

			saveFileDialog.FileName = String
				.Join("_", FileToSaveName.Split(invalidChars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
				.Trim();
		}

		if (saveFileDialog.ShowDialog() != true)
		{
			return;
		}

		if (File.Exists(saveFileDialog.FileName))
		{
			// Protect the user from accidental writes
			return;
		}

		try
		{
			GetStatistics(saveFileDialog.FileName);
		}
		catch (Exception e)
		{
			Debug.WriteLine(e);

			return;
		}

		SavedFileNotice = $"File {saveFileDialog.FileName} was saved.";
	}

	[RelayCommand]
	public void GetStatistics(string path)
	{
		using (var writer = new StreamWriter(path))

		using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
		{
			csv.Context.RegisterClassMap<TaskCardMap>();

			foreach (var taskList in Project.TaskBoard.TaskLists)
				csv.WriteRecords(taskList.TaskCards);
		}
	}

	[RelayCommand]
	public void ViewWorkersStatistic(User user)
	{
		var userStore = App.GetService<IUserStore>();
		userStore.CurrentUser = user;
		_navigationService.Navigate(typeof(UserStatisticsView));
	}
}

public class TaskCardMap : ClassMap<TaskCard>
{
	public TaskCardMap()
	{
		Map(i => i.Title);
		Map(i => i.Executor).Convert(i=>i.Value.Executor?.Name);
		Map(i => i.Description);
		Map(i => i.CreationTime);
		Map(i => i.ExpirationTime);
	}
}