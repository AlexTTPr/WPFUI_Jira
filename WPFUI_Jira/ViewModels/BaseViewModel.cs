using CommunityToolkit.Mvvm.ComponentModel;

namespace WPFUI_Jira.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    [ObservableProperty]
    private string _title;

    public bool IsNotBusy => !IsBusy;

	protected readonly INavigationService _navigationService;

    public BaseViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}