using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.Models.Stores;
internal class TaskListStore : ITaskListStore
{
	public TaskList CurrentTaskList { get; set; }

	public event Action CurrentTaskListChanged;
}
