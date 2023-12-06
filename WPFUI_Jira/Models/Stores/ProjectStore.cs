using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.Models.Stores;

class ProjectStore : IProjectStore
{
	public Project CurrentProject { get; set; }

	public event Action CurrentProjectChanged;
}
