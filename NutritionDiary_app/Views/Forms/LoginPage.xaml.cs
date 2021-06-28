using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NutritionDiary_app.AppLayout.Views;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Services;
using NutritionDiary_app.ViewModels.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NutritionDiary_app.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [Preserve(AllMembers = true)]
    public partial class LoginPage : ContentPage
    {
        //private readonly ApiService service = new ApiService();

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            //try { await service.GetProfileAsync(Settings.AccessToken); }
            //catch (NullReferenceException ex) { Debug.WriteLine(ex.Message); }

            //await Navigation.PushAsync(new AddProfilePage());

            if (Settings.Result == 0)
            {
                await Navigation.PushAsync(new AddProfilePage());
            }
            //if (Settings.UserId == null || Settings.IsValid == false)
            //{
            //    await Navigation.PushAsync(new RegisterPage());
            //}
            else await Navigation.PushAsync(new MainPageTabbedForm());
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
