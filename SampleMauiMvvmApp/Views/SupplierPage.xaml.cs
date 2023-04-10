using SampleMauiMvvmApp.ViewModels;

namespace SampleMauiMvvmApp.Views;

public partial class SupplierPage : ContentPage
{
	public SupplierPage(SupplierViewModel supplierViewModel)
	{
		InitializeComponent();
		BindingContext = supplierViewModel;
	}
}