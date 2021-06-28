using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionDiary_app.AppLayout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateFoodFromRation : ContentPage
    {
        private readonly ApiService service = new ApiService();
        private RationBindingModel _ration;
        private FoodBindingModel _food;

        public string FoodName { get; set; }
        public string MealTime { get; set; }
        public double Weight { get; set; }
        public DateTime Time { get; set; }

        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbs { get; set; }

        public List<string> Meals { get; set; }

        public UpdateFoodFromRation(FoodBindingModel food, RationBindingModel ration)
        {
            InitializeComponent();

            _food = food;
            _ration = ration;

            Meals = new List<string>
            {
                "Завтрак",
                "Второй завтрак",
                "Обед",
                "Второй обед",
                "Ужин",
                "Второй ужин",
            };

            FoodName = food.FoodName;
            FoodName_lbl.Text = FoodName;

            Calories = food.Calories;
            Proteins = food.Proteins;
            Fats = food.Fats;
            Carbs = food.Carbs;

            MealsPicker.SelectedItem = MealTime;

            MealsPicker.ItemsSource = Meals;

            WeightEntry.Placeholder = ration.Weight.ToString();
            WeightEntry.Text = WeightEntry.Placeholder;
        }

        private async void Put_Clicked(object sender, EventArgs e)
        {
            TimeSpan _time = timePicker.Time;
            Time += _time;

            Weight = double.Parse(WeightEntry.Text);

            var model = new RationBindingModel
            {
                CaloriesInFood = Calories,
                ProteinsInFood = Proteins,
                FatsInFood = Fats,
                CarbsInFood = Carbs,
                Meal = MealTime,
                FoodName = FoodName,
                Weight = Weight,
                Time = Time,
                Date = DateTime.Now,
                Id = _ration.Id
            };

            await service.UpdateFoodFromRationAsync(_ration.Id, model, Settings.AccessToken);

            await Navigation.PushAsync(new MainPageTabbedForm());
        }

        private void MealsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            MealTime = MealsPicker.Items[MealsPicker.SelectedIndex];
        }


        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void FoodName_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Info",
                $"Калорийность: {_food.Calories:0} \n" +
                $"Белки: {_food.Proteins:0} \n" +
                $"Жиры: {_food.Fats:0} \n" +
                $"Углеводы: {_food.Carbs:0}",
                "OK");
        }
    }
}
