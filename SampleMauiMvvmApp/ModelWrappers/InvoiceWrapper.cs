using CommunityToolkit.Mvvm.ComponentModel;
using SampleMauiMvvmApp.Models;

namespace SampleMauiMvvmApp.ModelWrappers
{
    public partial class InvoiceWrapper : ObservableObject
    {
        public InvoiceWrapper(Invoice invoiceModel)
        {
            if (invoiceModel != null)
            {
                Id = invoiceModel.Id;
                InvoiceNo = invoiceModel.InvoiceNo;
                InvoiceDate = invoiceModel.InvoiceDate;
                InvoiceTotal = invoiceModel.InvoiceTotal;
                SupplierId = invoiceModel.SupplierId;
            }
        }

        public int Id { get; set; }

        [ObservableProperty]
        string invoiceNo;

        [ObservableProperty]
        DateTime invoiceDate;

        [ObservableProperty]
        Decimal invoiceTotal;

        [ObservableProperty]
        int supplierId;

        [ObservableProperty]
        bool isNew;
    }
}
