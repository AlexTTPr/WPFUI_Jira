using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models.Services.Interfaces;

namespace WPFUI_Jira.Models.Services;
public class TaskListService : ITaskListService
{
    private IDbRepos _db;

    public TaskListService(IDbRepos dbRepos)
    {
        _db = dbRepos;
    }

    public void CreateTaskList(TaskList taskCard)
    {
        _db.TaskLists.Create(taskCard);
        Save();
    }

    public void DeleteTaskList(int id)
    {
        _db.TaskLists.Delete(id);
        Save();
    }

    public TaskList GetTaskList(int id)
    {
        return _db.TaskLists.GetItem(id);
    }

    public IEnumerable<TaskList> GetTaskLists(int ownerId)
    {
        return _db.TaskLists.GetCollection(ownerId);
    }

    public void UpdateTaskList(TaskList taskCard)
    {
        _db.TaskLists.Update(taskCard);
        Save();
    }

    public void Save()
    {
        _db.Save();
    }
}
