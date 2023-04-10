using SampleMauiMvvmApp.ViewModels;

namespace SampleMauiMvvmApp.Views;

public partial class SupplierUpdatePage : ContentPage
{
	public SupplierUpdatePage(SupplierUpdateViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}