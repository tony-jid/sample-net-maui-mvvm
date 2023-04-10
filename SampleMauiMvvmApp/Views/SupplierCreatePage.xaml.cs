using SampleMauiMvvmApp.Fakers;
using SampleMauiMvvmApp.ViewModels;

namespace SampleMauiMvvmApp.Views;

public partial class SupplierCreatePage : ContentPage
{
	public SupplierCreatePage(SupplierCreateViewModel vm)
	{
		InitializeComponent();
		
		SupplierFaker supplierFaker = new();
		vm.Supplier = supplierFaker.Generate();

		BindingContext = vm;
	}
}