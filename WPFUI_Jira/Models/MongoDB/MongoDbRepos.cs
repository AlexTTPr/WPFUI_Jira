using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository;
using WPFUI_Jira.Models.Repository.Interfaces;

namespace WPFUI_Jira.Models.MongoDB;

class MongoDbRepos : IDbRepos
{
	private MongoContext _context;

	private UserRepositoryMongo _userRepository;
	private ProjectRepositoryMongo _projectRepository;
	private TaskBoardRepositoryMongo _boardRepository;
	private TaskListRepositoryMongo _listRepository;
	private TaskCardRepositoryMongo _cardRepository;

	public MongoDbRepos()
	{
		_context = new MongoContext();
		_userRepository = new UserRepositoryMongo(_context);
		_projectRepository = new ProjectRepositoryMongo(_context);
		_boardRepository = new TaskBoardRepositoryMongo(_context);
		_listRepository = new TaskListRepositoryMongo(_context);
		_cardRepository = new TaskCardRepositoryMongo(_context);
	}

	public IRepository<User> Users
	{
		get
		{
			return _userRepository;
		}
	}

	public IRepository<Project> Projects
	{
		get
		{
			return _projectRepository;
		}
	}

	public IRepository<TaskBoard> TaskBoards
	{
		get
		{
			return _boardRepository;
		}
	}

	public IRepository<TaskList> TaskLists
	{
		get
		{
			return _listRepository;
		}
	}

	public IRepository<TaskCard> TaskCards
	{
		get
		{
			return _cardRepository;
		}
	}

	public void Save()
	{
		return;
	}
}
