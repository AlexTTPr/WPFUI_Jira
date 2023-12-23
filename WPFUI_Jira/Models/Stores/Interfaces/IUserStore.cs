using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Stores.Interfaces;
public interface IUserStore
{
	User CurrentUser { get; set; }

	event Action CurrentUserChanged;
}
