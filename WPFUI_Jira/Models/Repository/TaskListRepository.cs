using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.Repository;
internal class TaskListRepository : IRepository<TaskList>
{
	private ApplicationContext _context;

	public TaskListRepository(ApplicationContext context)
	{
		_context = context;
	}

	public void Create(TaskList item)
	{
		_context.TaskLists.Add(item);
	}

	public void Delete(int Id)
	{
		var item = _context.TaskLists.Find(Id);
		if (item != null)
			_context.TaskLists.Remove(item);
	}

	public ICollection<TaskList> GetCollection(int ownerId)
	{
		return _context.TaskLists.Where(c => c.TaskBoardId == ownerId).ToList();
	}

	public TaskList GetItem(int Id)
	{
		return _context.TaskLists.Find(Id);
	}

	public void Update(TaskList item)
	{
		_context.Entry(item).State = EntityState.Modified;
	}
}
