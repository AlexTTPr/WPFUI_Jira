using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models.Services.Interfaces;

namespace WPFUI_Jira.Models.Services;
public class TaskCardService : ITaskCardService
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

	public TaskCard PutTask(TaskCard taskCard)
    {
        if (taskCard == null)
            throw new ArgumentNullException(nameof(taskCard));
        if (string.IsNullOrEmpty(taskCard.Title))
            throw new InvalidOperationException("Title shouldn't be null or empty");
        if (taskCard.ExpirationTime < DateTime.Now)
            throw new InvalidOperationException("ExpirationDate should be greater than currenr date");
        
        if(taskCard.Id == 0)
            CreateTaskCard(taskCard);
        else
            UpdateTaskCard(taskCard);

        return GetTaskCard(taskCard.Id);
    }

    public void Save()
    {
        _db.Save();
    }
}