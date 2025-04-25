using Foundation;

namespace EcommerceApp
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => COP4870.ECommerce.UI.MAUI.MauiProgram.CreateMauiApp();
    }
}
