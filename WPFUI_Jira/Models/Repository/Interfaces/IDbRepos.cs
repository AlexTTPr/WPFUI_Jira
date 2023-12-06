using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Repository.Interfaces;

public interface IDbRepos
{
    IRepository<User> Users { get; }

    IRepository<Project> Projects { get; }

    IRepository<TaskBoard> TaskBoards { get; }

    IRepository<TaskList> TaskLists { get; }

    IRepository<TaskCard> TaskCards { get; }

    void Save();
}
