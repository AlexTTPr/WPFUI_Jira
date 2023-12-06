using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models.Services.Interfaces;

namespace WPFUI_Jira.Models.Services;

internal class TaskBoardService : ITaskBoardService
{
    private IDbRepos _db;

    public TaskBoardService(IDbRepos dbRepos)
    {
        _db = dbRepos;
    }

    public void CreateTaskBoard(TaskBoard taskBoard)
    {
        _db.TaskBoards.Create(taskBoard);
        Save();
    }

    public void DeleteTaskBoard(int id)
    {
        _db.TaskBoards.Delete(id);
        Save();
    }

    public TaskBoard GetTaskBoard(int id)
    {
        return _db.TaskBoards.GetItem(id);
    }

    public IEnumerable<TaskBoard> GetTaskBoards(int ownerId)
    {
        return _db.TaskBoards.GetCollection(ownerId);
    }

    public void UpdateTaskBoard(TaskBoard taskBoard)
    {
        _db.TaskBoards.Update(taskBoard);
        Save();
    }

    public void Save()
    {
        _db.Save();
    }
}
