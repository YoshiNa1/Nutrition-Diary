using Foundation;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using Syncfusion.SfRating.XForms.iOS;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.BadgeView;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.Expander;
using Syncfusion.XForms.iOS.Graphics;
using Syncfusion.XForms.iOS.PopupLayout;
using Syncfusion.XForms.iOS.ProgressBar;
using Syncfusion.XForms.iOS.TabView;
using Syncfusion.SfGauge.XForms.iOS;
using UIKit;
using Xamarin.Forms;
using System.Net;

namespace NutritionDiary_app.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDU2MjY3QDMxMzkyZTMxMmUzMGhtRHdQN0xvNXM1VEtETnVVcGdvbUorMG9Lb2o0MzhPOEtzSUdMdjJ3RWc9");

            Forms.SetFlags("CollectionView_Experimental");
            Forms.Init();
#if DEBUG
            ServicePointManager
            .ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
#endif

            SfButtonRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfBorderRenderer.Init();
            SfGradientViewRenderer.Init();
            SfListViewRenderer.Init();
            SfRatingRenderer.Init();
            SfRotatorRenderer.Init();
            SfComboBoxRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfPopupLayoutRenderer.Init();
            SfExpanderRenderer.Init();
            SfCardViewRenderer.Init();
            SfBadgeViewRenderer.Init();
            SfSegmentedControlRenderer.Init();
            Core.Init();
            SfTabViewRenderer.Init();
            SfCalendarRenderer.Init();
            SfLinearProgressBarRenderer.Init();
            SfChartRenderer.Init();
            SfGaugeRenderer.Init();
            LoadApplication(new App());

            ////UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            ////if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            ////{
            ////    statusBar.BackgroundColor = Color.FromHex("#e83f94").ToUIColor();
            ////    statusBar.TintColor = UIColor.White;
            ////}

            var result = base.FinishedLaunching(app, options);

            var safeAreInset = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;

            //if (safeAreInset.Top > 0)
            //{
            //    AppSettings.Instance.IsSafeAreaEnabled = true;
            //    AppSettings.Instance.SafeAreaHeight = safeAreInset.Top;
            //}

            return result;
        }
    }
}
