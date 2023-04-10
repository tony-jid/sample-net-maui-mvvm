using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SampleMauiMvvmApp.Messages;
using SampleMauiMvvmApp.Models;
using SampleMauiMvvmApp.ModelWrappers;
using SampleMauiMvvmApp.Services;
using SampleMauiMvvmApp.Views;

namespace SampleMauiMvvmApp.ViewModels
{
    public partial class SupplierViewModel : BaseViewModel
    {
        // If properties of Supplier model do not implement [ObservableProperty]
        // Then ObservableCollection<Supplier> will not detect any changes of Supplier's properties and UI will not be updated
        // Therefore, SupplierWrapper with ObservableObject and ObservableProperty is needed to facilitate the UI update
        //public ObservableCollection<Supplier> Suppliers { get; set; } = new();
        public ObservableCollection<SupplierWrapper> Suppliers { get; set; } = new();

        readonly SupplierService _supplierService;

        public SupplierViewModel(SupplierService supplierService)
        {
            Title = "Supplier Page";
            _supplierService = supplierService;

            WeakReferenceMessenger.Default.Register<SupplierCreateMessage>(this, (obj, handler) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var newSupplier = new SupplierWrapper(handler.Value)
                    {
                        IsNew = true
                    };
                    Suppliers.Insert(0, newSupplier);
                });
            });

            WeakReferenceMessenger.Default.Register<SupplierUpdateMessage>(this, (obj, handler) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var updatedSupplier = Suppliers.FirstOrDefault(x => x.Id == handler.Value.Id);
                    updatedSupplier.Name = handler.Value.Name;
                    updatedSupplier.Abn = handler.Value.Abn;
                    updatedSupplier.Email = handler.Value.Email;
                    updatedSupplier.Phone = handler.Value.Phone;
                    updatedSupplier.Address = handler.Value.Address;
                    updatedSupplier.IsUpdated = true;
                });
            });
        }

        [ObservableProperty]
        public bool isRefreshing;

        [RelayCommand]
        public async Task GetSuppliersAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                Suppliers.Clear();
                await Task.Delay(500);

                var suppliers = await _supplierService.GetAllSuppliers();
                if (suppliers == null)
                {
                    await Shell.Current.DisplayAlert("Error", _supplierService.StatusMessage, "OK");
                }
                else
                {
                    suppliers.ForEach(x => { Suppliers.Add(new SupplierWrapper(x)); });
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GoToDetailsAsync(int supplierId)
        {
            if (supplierId <= 0) return;

            var supplier = await _supplierService.GetSupplierDetails(supplierId);
            if (supplier == null)
            {
                await Shell.Current.DisplayAlert("Error", "Failed getting supplier details", "OK");
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(SupplierDetailPage)}", true,
                new Dictionary<string, object>()
                {
                    { nameof(Supplier), new SupplierWrapper(supplier) }
                });
        }

        [RelayCommand]
        public async Task GoToCreateAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(SupplierCreatePage)}", true);
        }

        [RelayCommand]
        public async Task DeleteSupplierAsync(SupplierWrapper supplier)
        {
            await _supplierService.DeleteSupplier(Models.Supplier.GenerateNewFromWrapper(supplier));
            Suppliers.Remove(supplier);
        }

        [RelayCommand]
        public async Task UpdateSupplierAsync(SupplierWrapper supplier)
        {
            await Shell.Current.GoToAsync($"{nameof(SupplierUpdatePage)}", true,
                new Dictionary<string, object>()
                {
                    { nameof(Supplier), supplier }
                });
        }
    }
}
