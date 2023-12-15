using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.MongoDB;

class TaskListRepositoryMongo : IRepository<TaskList>
{
	private MongoContext _context;
    public TaskListRepositoryMongo(MongoContext context)
    {
        _context = context;
    }

    public void Create(TaskList item)
	{
		throw new NotImplementedException();
	}

	public void Delete(int Id)
	{
		throw new NotImplementedException();
	}

	public ICollection<TaskList> GetCollection(int ownerId)
	{
		var builder = new FilterDefinitionBuilder<TaskList>();
		var filter = builder.Empty;

		return _context.TaskListCollection.Find(filter).ToList().Where(c => c.TaskBoard.Id == ownerId).ToList();
	}

	public TaskList GetItem(int Id)
	{
		throw new NotImplementedException();
	}

	public void Update(TaskList item)
	{
		throw new NotImplementedException();
	}
}
