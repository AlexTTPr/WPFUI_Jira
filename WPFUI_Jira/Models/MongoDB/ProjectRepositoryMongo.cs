using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.MongoDB;

class ProjectRepositoryMongo : IRepository<Project>
{
	private MongoContext _context;

	public ProjectRepositoryMongo(MongoContext context)
	{
		_context = context;
	}

	public void Create(Project item)
	{
		throw new NotImplementedException();
	}

	public void Delete(int Id)
	{
		throw new NotImplementedException();
	}

	public ICollection<Project> GetCollection(int ownerId)
	{
		var builder = new FilterDefinitionBuilder<Project>();
		var filter = builder.Empty;

		return _context.ProjectCollection.Find(filter).ToList().Where(c => c.Owner.Id == ownerId || c.Workers.Any(n => n.Id == ownerId)).ToList();
	}

	public Project GetItem(int Id)
	{
		return _context.ProjectCollection.Find(i => i.Id == Id).FirstOrDefault();
	}

	public void Update(Project item)
	{
		throw new NotImplementedException();
	}
}
