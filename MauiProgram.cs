using COP4870.ECommerce.Services;
using Microsoft.Extensions.Logging;

namespace COP4870.ECommerce.UI.MAUI
{
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

            // Register services as singletons to maintain state across the app
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<ShoppingCartService>();

            // Register pages
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<InventoryPage>();
            builder.Services.AddTransient<ProductDetailPage>();
            builder.Services.AddTransient<ShoppingCartPage>();
            builder.Services.AddTransient<CheckoutPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}