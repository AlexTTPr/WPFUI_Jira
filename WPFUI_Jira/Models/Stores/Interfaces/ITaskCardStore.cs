using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI_Jira.Models.Stores.Interfaces;

public interface ITaskCardStore
{
	TaskCard CurrentTaskCard { get; set; }

	event Action CurrentTaskCardChanged;
}
