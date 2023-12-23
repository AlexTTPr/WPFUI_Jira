using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.Repository;

internal class TaskBoardRepository : IRepository<TaskBoard>
{
    private ApplicationContext _context;

    public TaskBoardRepository(ApplicationContext applicationContext)
    {
        _context = applicationContext;
    }

    public void Create(TaskBoard item)
    {
        _context.TaskBoards.Add(item);
    }

    public void Delete(int Id)
    {
		var item = _context.TaskBoards.Find(Id);
		if (item != null)
			_context.TaskBoards.Remove(item);
	}

    public ICollection<TaskBoard> GetCollection(int ownerId)
    {
        return _context.TaskBoards.Where(c => c.Owner.Id == ownerId).ToList();
    }

    public TaskBoard GetItem(int Id)
    {
        return _context.TaskBoards.Find(Id);
	}

    public void Update(TaskBoard item)
    {
		_context.TaskBoards.Update(item);
	}
}
