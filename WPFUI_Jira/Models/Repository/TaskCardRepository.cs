using Microsoft.EntityFrameworkCore;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.Repository;
internal class TaskCardRepository : IRepository<TaskCard>
{
	private ApplicationContext _context;

    public TaskCardRepository(ApplicationContext context)
    {
        _context = context;
	}

    public void Create(TaskCard item)
	{
		_context.TaskCards.Add(item);
	}

	public void Delete(int Id)
	{
		var item = _context.TaskCards.Find(Id);
		if (item != null)
			_context.TaskCards.Remove(item);
	}

	public ICollection<TaskCard> GetCollection(int ownerId)
	{
		return _context.TaskCards.Where(c => c.TaskListId == ownerId).Include(c => c.Executor).ToList();
	}

	public TaskCard GetItem(int Id)
	{
		return _context.TaskCards.Where(c => c.Id == Id).Include(c => c.Executor).First();
	}

	public void Update(TaskCard item)
	{
		_context.TaskCards.Update(item);
	}
}

