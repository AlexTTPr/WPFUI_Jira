using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.Repository;
internal class UserRepository : IRepository<User>
{
	private ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public void Create(User item)
	{
		_context.Users.Add(item);
	}

	public void Delete(int Id)
	{
		var item = _context.Users.Find(Id);
		if (item != null)
			_context.Users.Remove(item);
	}

	public ICollection<User> GetCollection(int projectId)
	{
		return _context.Users.Where(c => c.WorkProjects.Any(e => e.Id == projectId)).ToList();
	}

	public User GetItem(int Id)
	{
		return _context.Users.AsNoTracking().Where(c => c.Id == Id).Single();
	}

	public void Update(User item)
	{
		_context.Users.Update(item);
	}
}
