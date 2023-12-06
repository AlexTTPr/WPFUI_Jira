using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.Models.Stores;

//need some polishing
public class AccountStore : IAccountStore
{
	private User _currentUser;
	public User CurrentUser
	{
		get
		{
			return _currentUser;
		}
		set
		{
			_currentUser = value;
		}
	}

	public event Action CurrentUserChanged;
}
