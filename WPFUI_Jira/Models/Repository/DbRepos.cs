using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Views;

namespace WPFUI_Jira.Models.Repository;
internal class DbRepos : IDbRepos
{
    private ApplicationContext _context;

    private UserRepository? _userRepository;
    private ProjectRepository? _projectRepository;
    private TaskBoardRepository? _boardRepository;
    private TaskListRepository? _listRepository;
    private TaskCardRepository? _cardRepository;
    private ActionRecordRepository? _actionRecordRepository;

    public DbRepos()
    {
        _context = new ApplicationContext();
    }

    public IRepository<User> Users
    {
        get
        {
            _userRepository ??= new UserRepository(_context);
            return _userRepository;
        }
    }

    public IRepository<Project> Projects
    {
        get
        {
            _projectRepository ??= new ProjectRepository(_context);
            return _projectRepository;
        }
    }

    public IRepository<TaskBoard> TaskBoards
    {
        get
        {
            _boardRepository ??= new TaskBoardRepository(_context);
            return _boardRepository;
        }
    }

    public IRepository<TaskList> TaskLists
    {
        get
        {
            _listRepository ??= new TaskListRepository(_context);
            return _listRepository;
        }
    }

    public IRepository<TaskCard> TaskCards
    {
        get
        {
            _cardRepository ??= new TaskCardRepository(_context);
            return _cardRepository;
        }
    }

	public IRepository<ActionRecord> ActionRecords
	{
		get
		{
			_actionRecordRepository ??= new ActionRecordRepository(_context);
			return _actionRecordRepository;
		}
	}

	public void Save()
    {
        _context.SaveChanges();
    }
}
