using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.ViewModels;

public partial class TaskCardDetailsViewModel : BaseViewModel
{
	private ITaskCardStore _taskCardStore;

	[ObservableProperty]
	private string _title;

	[ObservableProperty]
	private string? _description;

	[ObservableProperty]
	private User? _executor;

	[ObservableProperty]
	private DateTime? _expirationTime;

	[ObservableProperty]
	private DateTime? _creationTime;

	public TaskCardDetailsViewModel(ITaskCardStore taskCardStore, INavigationService navigationService) : base(navigationService)
	{
		_taskCardStore = taskCardStore;

		var taskCard = _taskCardStore.CurrentTaskCard;
		Title = taskCard.Title;
		Description = taskCard.Description;
		Executor = taskCard.Executor;
		ExpirationTime = taskCard.ExpirationTime;
		CreationTime = taskCard.CreationTime;
	}
}
