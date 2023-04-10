using SampleMauiMvvmApp.ViewModels;

namespace SampleMauiMvvmApp.Views;

//[QueryProperty("SupplierId", "id")]
public partial class SupplierDetailPage : ContentPage
{
	public int SupplierId { get; set; }

	public SupplierDetailPage(SupplierDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}