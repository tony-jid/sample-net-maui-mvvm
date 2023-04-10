using SampleMauiMvvmApp.Fakers;
using SampleMauiMvvmApp.ModelWrappers;
using SampleMauiMvvmApp.ViewModels;

namespace SampleMauiMvvmApp.Views;

public partial class InvoiceCreatePage : ContentPage
{
	public InvoiceCreatePage(InvoiceCreateViewModel vm)
	{
		InitializeComponent();

		InvoiceFaker invoiceFaker = new();
		vm.VmInvoice = new InvoiceWrapper(invoiceFaker.Generate());

		BindingContext = vm;
	}
}