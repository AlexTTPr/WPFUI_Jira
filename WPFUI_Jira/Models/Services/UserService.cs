using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Repository.Interfaces;
using WPFUI_Jira.Models.Services.Interfaces;

namespace WPFUI_Jira.Models.Services;

public class UserService : IUserService
{
    private IDbRepos _db;

    public UserService(IDbRepos dbRepos)
    {
        _db = dbRepos;
    }

    public void CreateUser(User user)
    {
        _db.Users.Create(user);
        Save();
    }

    public void DeleteUser(int id)
    {
        _db.Users.Delete(id);
        Save();
    }

    public User GetUser(int id)
    {
        return _db.Users.GetItem(id);
    }

    public ICollection<User> GetUsers(int projectId)
    {
        return _db.Users.GetCollection(projectId);
    }

    public void UpdateUser(User user)
    {
        _db.Users.Update(user);
        Save();
    }

    public void Save()
    {
        _db.Save();
    }
}
