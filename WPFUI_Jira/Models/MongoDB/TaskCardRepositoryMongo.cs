using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.MongoDB;

class TaskCardRepositoryMongo : IRepository<TaskCard>
{
	private MongoContext _context;

    public TaskCardRepositoryMongo(MongoContext context)
    {
		_context = context;
    }

    public void Create(TaskCard item)
	{
		TaskCard last = _context.TaskCardCollection.Find(new FilterDefinitionBuilder<TaskCard>().Empty).SortByDescending(i => i.Id).Limit(1).FirstOrDefault();
		item.Id = last != null ? last.Id + 1 : 1;
		_context.TaskCardCollection.InsertOneAsync(item);
	}

	public void Delete(int Id)
	{
		_context.TaskCardCollection.DeleteOneAsync(i => i.Id == Id);
	}

	public ICollection<TaskCard> GetCollection(int ownerId)
	{
		var builder = new FilterDefinitionBuilder<TaskCard>();	
		var filter = builder.Empty;

		return _context.TaskCardCollection.Find(filter).ToList().Where(c => c.TaskListId == ownerId).ToList();
	}

	public TaskCard GetItem(int Id)
	{
		return _context.TaskCardCollection.Find(c=>c.Id==Id).First();
	}

	public void Update(TaskCard item)
	{
		_context.TaskCardCollection.ReplaceOneAsync(c => c.Id == item.Id, item);
	}
}
