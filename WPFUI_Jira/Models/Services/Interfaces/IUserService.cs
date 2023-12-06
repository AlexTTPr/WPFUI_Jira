using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Services.Interfaces;

internal interface IUserService
{
    IEnumerable<User> GetUsers(int projectId);

    User GetUser(int id);

    void CreateUser(User user);

    void UpdateUser(User user);

    void DeleteUser(int id);
}
