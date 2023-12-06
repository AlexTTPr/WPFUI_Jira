using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Stores.Interfaces;
using WPFUI_Jira.ViewModels;

namespace WPFUI_Jira.Models.Stores;

class NavigationStore : INavigationStore
{
    private BaseViewModel _currentViewModel;

    public BaseViewModel CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            CurrentViewModelChanged?.Invoke();
        }
    }

    public event Action CurrentViewModelChanged;

    public NavigationStore()
    {
        
    }
}