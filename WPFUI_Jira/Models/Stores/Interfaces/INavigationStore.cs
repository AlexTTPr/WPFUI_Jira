using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.ViewModels;

namespace WPFUI_Jira.Models.Stores.Interfaces;

public interface INavigationStore
{
    BaseViewModel CurrentViewModel { get; set; }

    event Action CurrentViewModelChanged;
}
