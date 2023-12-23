using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.ViewModels;
public partial class UserStatisticsViewModel : BaseViewModel
{
	private IUserStore _userStore;
	private IProjectStore _projectStore;
	private IActionRecordService _actionRecordService;

	[ObservableProperty]
	private User _user;

	[ObservableProperty]
	private DateTime[] _dates;

	[ObservableProperty]
	private SeriesCollection _seriesCollection;

	public Func<double, string> FormatterX { get; set; }
	public Func<double, string> FormatterY { get; set; }

	public UserStatisticsViewModel(IProjectStore projectStore, IUserStore userStore, IActionRecordService actionRecordService, INavigationService navigationService) : base(navigationService)
	{
		_userStore = userStore;
		_projectStore = projectStore;
		_actionRecordService = actionRecordService;


		User = userStore.CurrentUser;

		Dates = new DateTime[7];
		for (int count = 0, i = -6; i <= 0; count++, i++)
			Dates[count] = DateTime.Now.AddDays(i);

		var dayConfig = Mappers.Xy<DateModel>()
		.X(dayModel => dayModel.DateTime.Ticks)
		.Y(dayModel => dayModel.Value);

		SeriesCollection = new SeriesCollection(dayConfig);	

		var line = new LineSeries()
		{
			Title = "Tracked time",
			Values = new ChartValues<DateModel>(),
			LineSmoothness = 0
		};

		foreach (var date in Dates)
		{
			var items = _actionRecordService.GetExecutorsActionRescordsInDate(User.Id, date);
			TimeSpan totalTime = new TimeSpan();

			foreach (var item in items)
				totalTime += item.TimeSpent;

			line.Values.Add(new DateModel() { DateTime = date.Date, Value = totalTime.Ticks});
		}

		FormatterX = value => new DateTime((long)value).ToString("dd|MM|yyyy", CultureInfo.InvariantCulture);
		FormatterY = value => new TimeSpan((long)value).ToString();

		SeriesCollection.Add(line);
	}

	public class DateModel
	{
		public DateTime DateTime { get; set; }
		public long Value { get; set; }
	}
}
