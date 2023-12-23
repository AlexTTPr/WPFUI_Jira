using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.Repository;

internal class ProjectRepository : IRepository<Project>
{
	private ApplicationContext _context;

	public ProjectRepository(ApplicationContext context)
	{
		_context = context;
	}

	public void Create(Project item)
	{
		_context.Projects.Add(item);
	}

	public void Delete(int Id)
	{
		var item = _context.Projects.Find(Id);
		if (item != null)
			_context.Projects.Remove(item);
	}

	public ICollection<Project> GetCollection(int ownerId)
	{
		return _context.Projects.Where(c => c.Owner.Id == ownerId || c.Workers.Any(n => n.Id == ownerId)).Include(c => c.Owner).ToList();
	}

	public Project GetItem(int Id)
	{
		return _context.Projects.Find(Id);
	}

	public void Update(Project item)
	{
		_context.Projects.Update(item);
	}
}
