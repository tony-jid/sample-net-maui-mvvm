using Ardalis.GuardClauses;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SampleMauiMvvmApp.Messages;
using SampleMauiMvvmApp.ModelWrappers;
using SampleMauiMvvmApp.Services;

namespace SampleMauiMvvmApp.ViewModels
{
    [QueryProperty("Supplier", "Supplier")]
    public partial class SupplierUpdateViewModel : BaseViewModel
    {
        readonly SupplierService _supplierService;

        public SupplierUpdateViewModel(SupplierService supplierService)
        {
            Title = "Update Supplier Page";
            _supplierService = supplierService;
        }

        [ObservableProperty]
        SupplierWrapper supplier;

        [RelayCommand]
        public async Task UpdateSupplierAsync()
        {
            if (IsValid())
            {
                var updatedSupplier = await _supplierService.UpdateSupplier(Models.Supplier.GenerateNewFromWrapper(Supplier));
                if (updatedSupplier != null)
                {
                    // Propagate the updated supplier to the main supplier page
                    WeakReferenceMessenger.Default.Send(new SupplierUpdateMessage(updatedSupplier));
                    await GoBackAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", _supplierService.StatusMessage, "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", StatusMessage, "OK");
            }
        }

        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public bool IsValid()
        {
            try
            {
                Guard.Against.NullOrWhiteSpace(Supplier.Name, nameof(Supplier.Name));
                Guard.Against.NullOrWhiteSpace(Supplier.Abn, nameof(Supplier.Abn));
                Guard.Against.NullOrWhiteSpace(Supplier.Phone, nameof(Supplier.Phone));
                Guard.Against.NullOrWhiteSpace(Supplier.Email, nameof(Supplier.Email));
                Guard.Against.NullOrWhiteSpace(Supplier.Address, nameof(Supplier.Address));
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                return false;
            }

            return true;
        }
    }
}
