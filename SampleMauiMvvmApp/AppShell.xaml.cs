using SampleMauiMvvmApp.Views;

namespace SampleMauiMvvmApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(SupplierDetailPage), typeof(SupplierDetailPage));
		Routing.RegisterRoute(nameof(SupplierCreatePage), typeof(SupplierCreatePage));
		Routing.RegisterRoute(nameof(SupplierUpdatePage), typeof(SupplierUpdatePage));
		Routing.RegisterRoute(nameof(InvoiceCreatePage), typeof(InvoiceCreatePage));
	}
}
