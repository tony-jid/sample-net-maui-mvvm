using Ardalis.GuardClauses;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SampleMauiMvvmApp.Messages;
using SampleMauiMvvmApp.ModelWrappers;
using SampleMauiMvvmApp.Services;

namespace SampleMauiMvvmApp.ViewModels
{
    [QueryProperty("SupplierId", "SupplierId")]
    public partial class InvoiceCreateViewModel : BaseViewModel
    {
        readonly InvoiceService _invoiceService;

        public InvoiceCreateViewModel(InvoiceService invoiceService)
        {
            Title = "Invoice Create Page";
            _invoiceService = invoiceService;
        }

        public int SupplierId { get; set; }

        [ObservableProperty]
        InvoiceWrapper vmInvoice;

        [RelayCommand]
        public async Task CreateInvoiceAsync()
        {
            if (IsValid())
            {
                VmInvoice.SupplierId = SupplierId;
                var newSupplier = await _invoiceService.InsertInvoice(Models.Invoice.GenerateNewFromWrapper(VmInvoice));
                if (newSupplier != null)
                {
                    // Propagate the new supplier to the main supplier page
                    WeakReferenceMessenger.Default.Send(new InvoiceCreateMessage(newSupplier));
                    await GoBackAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", _invoiceService.StatusMessage, "OK");
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
                Guard.Against.NullOrWhiteSpace(VmInvoice.InvoiceNo, nameof(VmInvoice.InvoiceNo));
                Guard.Against.OutOfRange<DateTime>(VmInvoice.InvoiceDate, nameof(VmInvoice.InvoiceDate), DateTime.MinValue, DateTime.MaxValue);
                Guard.Against.OutOfRange<Decimal>(VmInvoice.InvoiceTotal, nameof(VmInvoice.InvoiceTotal), 0, Decimal.MaxValue);
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
