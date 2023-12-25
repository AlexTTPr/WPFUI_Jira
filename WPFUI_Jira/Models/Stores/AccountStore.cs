using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Stores.Interfaces;
using WPFUI_Jira.Views;

namespace WPFUI_Jira.Models.Stores;

//need some polishing
public class AccountStore : IAccountStore
{
	private User _currentUser;
	public User CurrentAccount {  get=> _currentUser; set {
			_currentUser = value;
			CurrentAccountChanged?.Invoke();
		} }

	public event Action CurrentAccountChanged;
}
