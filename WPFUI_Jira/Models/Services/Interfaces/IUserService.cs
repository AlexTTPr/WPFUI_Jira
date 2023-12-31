﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Services.Interfaces;

public interface IUserService
{
    ICollection<User> GetUsers(int projectId);

    User GetUser(int id);

    void CreateUser(User user);

    void UpdateUser(User user);

    void DeleteUser(int id);
}
