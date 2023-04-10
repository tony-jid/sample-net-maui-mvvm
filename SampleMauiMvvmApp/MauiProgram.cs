using Microsoft.Extensions.Logging;
using SampleMauiMvvmApp.Services;
using SampleMauiMvvmApp.ViewModels;
using SampleMauiMvvmApp.Views;
using SQLite;

namespace SampleMauiMvvmApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		//builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		//builder.Services.AddSingleton<IMap>(Map.Default);

		builder.Services.AddSingleton<SupplierViewModel>();
		builder.Services.AddSingleton<SupplierPage>();

		builder.Services.AddTransient<SupplierDetailViewModel>();
		builder.Services.AddTransient<SupplierDetailPage>();

		builder.Services.AddTransient<SupplierCreateViewModel>();
		builder.Services.AddTransient<SupplierCreatePage>();

		builder.Services.AddTransient<SupplierUpdateViewModel>();
		builder.Services.AddTransient<SupplierUpdatePage>();

		builder.Services.AddTransient<InvoiceCreateViewModel>();
		builder.Services.AddTransient<InvoiceCreatePage>();

		builder.Services.AddSingleton<DbContext>();
		builder.Services.AddSingleton<SupplierService>();
		builder.Services.AddSingleton<InvoiceService>();

		return builder.Build();
	}
}
