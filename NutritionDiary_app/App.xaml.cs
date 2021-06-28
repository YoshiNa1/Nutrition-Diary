using System;
using System.Diagnostics;
using System.Reflection;
using NutritionDiary_app.AppLayout.Views;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Services;
using NutritionDiary_app.ViewModels.Forms;
using NutritionDiary_app.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NutritionDiary_app
{
    /// <summary>
    /// The UITemplate Application
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {

        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        private readonly ApiService service = new ApiService();

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDU2MjY3QDMxMzkyZTMxMmUzMGhtRHdQN0xvNXM1VEtETnVVcGdvbUorMG9Lb2o0MzhPOEtzSUdMdjJ3RWc9");
            
            InitializeComponent();

           // MainPage = new NavigationPage(new LoginPage());

           // GetProfile();

            SetMainPage();

        }


        private void SetMainPage()
        {
            if (!string.IsNullOrEmpty(Settings.AccessToken) && Settings.Result != 0)
            {
                if (Settings.AccessTokenExpirationDate < DateTime.UtcNow.AddHours(1))
                {
                    var signUpPageViewModel = new SignUpPageViewModel();
                    signUpPageViewModel.LoginCommand.Execute(null);
                }
                GetProfile();
                MainPage = new NavigationPage(new MainPageTabbedForm());
            }
            else if (Settings.UserId == null)
            {
                MainPage = new NavigationPage(new RegisterPage());
            }
            else if (!string.IsNullOrEmpty(Settings.Email)
                  && !string.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        private async void GetProfile()
        {
            try
            {
                await service.GetProfileAsync(Settings.AccessToken);
            }
            catch (NullReferenceException ex) { Debug.WriteLine(ex.Message); }
        }
        /// <summary>
        /// Invoked when your app starts
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Invoked when your app sleeps
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Invoked when your app resumes
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}

