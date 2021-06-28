using System;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Services;
using Xamarin.Forms;

namespace NutritionDiary_app.ViewModels
{
    public class AddFoodInDbViewModel : BaseViewModel
    {
        private readonly ApiService service = new ApiService();
        public string FoodName { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbs { get; set; }

        //public AddProfileViewModel()
        //{
        //    Gender = Settings.Gender;
        //    //profile = new ProfileBindingModel();
        //}


        public Command AddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await service.AddFoodInDbAsync
                    (FoodName, Calories, Proteins, Fats, Carbs, Settings.AccessToken);
                });
            }
        }
    }
}
