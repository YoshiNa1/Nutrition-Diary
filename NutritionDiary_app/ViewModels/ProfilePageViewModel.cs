using System;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NutritionDiary_app.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ProfilePageViewModel
    {
        private readonly ApiService service = new ApiService();

        public Command GetCommand
        {
            get
            {
                return new Command(async () =>
                {
                   await service.GetProfileAsync(Settings.AccessToken);
                });
            }
        }
    }
}
