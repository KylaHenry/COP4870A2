using Android.App;
using Android.Runtime;

namespace EcommerceApp
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => COP4870.ECommerce.UI.MAUI.MauiProgram.CreateMauiApp();
    }
}
