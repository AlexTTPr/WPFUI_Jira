using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models.Services.Interfaces;

namespace WPFUI_Jira.Models.Services;
internal class TaskCardService : ITaskCardService
{
    private IDbRepos _db;

    public TaskCardService(IDbRepos dbRepos)
    {
        _db = dbRepos;
    }

    public void CreateTaskCard(TaskCard taskCard)
    {
        _db.TaskCards.Create(taskCard);
        Save();
    }

    public void DeleteTaskCard(int id)
    {
        _db.TaskCards.Delete(id);
        Save();
    }


    public TaskCard GetTaskCard(int id)
    {
        return _db.TaskCards.GetItem(id);
    }

    public IEnumerable<TaskCard> GetTaskCards(int ownerId)
    {
        return _db.TaskCards.GetCollection(ownerId);
    }

    public void UpdateTaskCard(TaskCard taskCard)
    {
        _db.TaskCards.Update(taskCard);
        Save();
    }

    public void Save()
    {
        _db.Save();
    }
}


