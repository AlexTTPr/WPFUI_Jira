using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models;
using WPFUI_Jira.Models.Services.Interfaces;
using WPFUI_Jira.Models.Stores.Interfaces;
using WPFUI_Jira.ViewModels;

namespace WPFUI_Jira.Models.Services;

//WTF was inhereted from BaseViewModel?!
//class NavigationService : INavigationService
//{
//    private readonly INavigationStore _navigationStore;

//    public NavigationService(INavigationStore navigationStore)
//    {
//        _navigationStore = navigationStore;
//    }

//    public void Navigate(BaseViewModel baseViewModel)
//    {
//        _navigationStore.CurrentViewModel = baseViewModel;
//    }
//}