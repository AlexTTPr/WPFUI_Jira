using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.MongoDB;

class TaskBoardRepositoryMongo : IRepository<TaskBoard>
{
	private MongoContext _context;

    public TaskBoardRepositoryMongo(MongoContext context)
    {
        _context = context;
    }

    public void Create(TaskBoard item)
	{
		throw new NotImplementedException();
	}

	public void Delete(int Id)
	{
		throw new NotImplementedException();
	}

	public ICollection<TaskBoard> GetCollection(int ownerId)
	{
		var builder = new FilterDefinitionBuilder<TaskBoard>();
		var filter = builder.Empty;

		return _context.TaskBoardCollection.Find(filter).ToList().Where(c => c.Owner.Id == ownerId).ToList();
	}

	public TaskBoard GetItem(int Id)
	{
		throw new NotImplementedException();
	}

	public void Update(TaskBoard item)
	{
		throw new NotImplementedException();
	}
}
