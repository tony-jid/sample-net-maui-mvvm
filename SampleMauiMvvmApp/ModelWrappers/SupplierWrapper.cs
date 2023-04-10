using CommunityToolkit.Mvvm.ComponentModel;
using SampleMauiMvvmApp.Models;
using System.Collections.ObjectModel;

namespace SampleMauiMvvmApp.ModelWrappers
{
    public partial class SupplierWrapper : ObservableObject
    {
        public SupplierWrapper(Supplier supplierModel)
        {
            if (supplierModel != null)
            {
                Id = supplierModel.Id;
                Name = supplierModel.Name;
                Abn = supplierModel.Abn;
                Phone = supplierModel.Phone;
                Email = supplierModel.Email;
                Address = supplierModel.Address;

                Invoices = new();
                supplierModel.Invoices?.ForEach(x =>
                    Invoices.Add(new InvoiceWrapper(x))
                );
            }
        }

        public int Id { get; set; }
        public ObservableCollection<InvoiceWrapper> Invoices { get; set; }

        [ObservableProperty]
        string name;
        [ObservableProperty]
        string abn;
        [ObservableProperty]
        string phone;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string address;
        [ObservableProperty]
        bool isNew;
        [ObservableProperty]
        bool isUpdated;
    }
}
