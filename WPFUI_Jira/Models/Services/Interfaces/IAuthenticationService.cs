using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.Models.Services.Interfaces;

public interface IAuthenticationService
{
    IAccountStore AccountStore { get; }

    bool IsLoggedIn { get; }

    void Register();

    void Login();

    void Logout();
}
