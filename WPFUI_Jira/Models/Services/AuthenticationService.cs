using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.Models.Services;
internal class AuthenticationService : IAuthenticationService
{
	public IAccountStore AccountStore { get; set; }

	public bool IsLoggedIn { get; private set; }

    public AuthenticationService(IAccountStore accountStore)
    {
        AccountStore = accountStore;
    }

    public void Login()
	{
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
	}

	public void Logout()
	{
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
	}

	public void Register()
	{
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
		throw new NotImplementedException();
	}
}