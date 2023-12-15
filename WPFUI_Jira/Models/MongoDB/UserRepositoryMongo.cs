using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.MongoDB;

class UserRepositoryMongo : IRepository<User>
{
	private MongoContext _context;

    public UserRepositoryMongo(MongoContext context)
    {
        _context = context;
    }

    public void Create(User item)
	{
		throw new NotImplementedException();
	}

	public void Delete(int Id)
	{
		throw new NotImplementedException();
	}

	public ICollection<User> GetCollection(int projectId)
	{
		return _context.UserCollection.Find(i => i.Id == 2).ToList();
	}

	public User GetItem(int Id)
	{
		var v = _context.UserCollection.Find(new FilterDefinitionBuilder<User>().Empty).ToList();

		return _context.UserCollection.Find(i => i.Id == Id).First();
	}

	public void Update(User item)
	{
		_context.UserCollection.ReplaceOneAsync(c=>c.Id==item.Id, item);
	}
}
