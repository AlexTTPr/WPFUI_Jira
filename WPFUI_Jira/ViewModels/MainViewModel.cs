using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI_Jira.Models.Stores.Interfaces;

namespace WPFUI_Jira.ViewModels;

public partial class MainViewModel : BaseViewModel
{
	private readonly INavigationStore _navigationStore;

	[ObservableProperty]
	public BaseViewModel _currentViewModel;

    public MainViewModel(INavigationStore navigationStore, NavigationService navigationService) : base(navigationService)
	{
        _navigationStore = navigationStore;
		_navigationStore.CurrentViewModelChanged += () => CurrentViewModel = _navigationStore.CurrentViewModel;
		CurrentViewModel = _navigationStore.CurrentViewModel;
	}
}
