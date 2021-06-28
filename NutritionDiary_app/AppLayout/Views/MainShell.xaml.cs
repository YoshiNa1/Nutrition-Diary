using System;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionDiary_app.AppLayout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainShell : Shell
    {
        public MainShell()
        {
            InitializeComponent();
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Settings.ClearSettings();
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
