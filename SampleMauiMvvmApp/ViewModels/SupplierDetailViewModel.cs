using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SampleMauiMvvmApp.Messages;
using SampleMauiMvvmApp.Models;
using SampleMauiMvvmApp.ModelWrappers;
using SampleMauiMvvmApp.Views;

namespace SampleMauiMvvmApp.ViewModels
{
    [QueryProperty("Supplier", "Supplier")]
    public partial class SupplierDetailViewModel : BaseViewModel
    {
        public SupplierDetailViewModel()
        {
            Title = "Supplier Detail Page";

            WeakReferenceMessenger.Default.Register<InvoiceCreateMessage>(this, (obj, handler) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var newInvoice = new InvoiceWrapper(handler.Value)
                    {
                        IsNew = true
                    };

                    if (Supplier.Invoices == null) Supplier.Invoices = new();
                    Supplier.Invoices.Insert(0, newInvoice);

                });
            });
        }

        [ObservableProperty]
        SupplierWrapper supplier;

        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task GoToCreateInvoiceAsync(int supplierId)
        {
            await Shell.Current.GoToAsync($"{nameof(InvoiceCreatePage)}?SupplierId={supplierId}", true,
                new Dictionary<string, object>()
                {
                    { nameof(Supplier), this.Supplier }
                }
            );
        }
    }
}
