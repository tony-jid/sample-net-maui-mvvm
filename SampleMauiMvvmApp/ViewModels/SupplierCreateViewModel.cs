using Ardalis.GuardClauses;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SampleMauiMvvmApp.Messages;
using SampleMauiMvvmApp.Models;
using SampleMauiMvvmApp.Services;

namespace SampleMauiMvvmApp.ViewModels
{
    public partial class SupplierCreateViewModel : BaseViewModel
    {
        SupplierService _supplierService;

        public SupplierCreateViewModel(SupplierService supplierService)
        {
            Title = "Create New Supplier";
            _supplierService = supplierService;
        }

        [ObservableProperty]
        Supplier supplier;

        [RelayCommand]
        public async Task CreateSupplierAsync()
        {
            if (IsValid())
            {
                var newSupplier = await _supplierService.InsertSupplier(Supplier);
                if (newSupplier != null)
                {
                    // Propagate the new supplier to the main supplier page
                    WeakReferenceMessenger.Default.Send(new SupplierCreateMessage(newSupplier));
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
