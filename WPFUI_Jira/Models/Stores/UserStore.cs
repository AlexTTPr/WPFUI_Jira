using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.Models.Stores;
public class UserStore : IUserStore
{
	public User CurrentUser { get; set; }

	public event Action CurrentUserChanged;
}
