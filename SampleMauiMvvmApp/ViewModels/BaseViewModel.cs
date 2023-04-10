using CommunityToolkit.Mvvm.ComponentModel;

namespace SampleMauiMvvmApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {

        }

        [ObservableProperty]
        string title;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        string statusMessage;
    }
}
