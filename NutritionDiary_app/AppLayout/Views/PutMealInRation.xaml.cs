using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NutritionDiary_app.AppLayout.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PutMealInRation : ContentPage
    {

        private readonly ApiService service = new ApiService();
        private RationBindingModel ration;
        private MealsBindingModel _meal;

        public string MealName { get; set; }
        public string MealTime { get; set; }
        public string TotalWeight { get; set; }
        public double Weight { get; set; }
        public DateTime Time { get; set; }

        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbs { get; set; }

        public ObservableCollection<FoodBindingModel> Foods { get; set; }
        public List<FoodBindingModel> _Foods { get; set; }
        public List<string> Meals { get; set; }

        public PutMealInRation(MealsBindingModel meal)
        {
            InitializeComponent();

            _meal = meal;

            GetMealFoods();

            Meals = new List<string>
            {
                "Завтрак",
                "Второй завтрак",
                "Обед",
                "Второй обед",
                "Ужин",
                "Второй ужин",
            };

            MealName = meal.MealName;
            MealName_lbl.Text = MealName;

            WeightEntry.Placeholder = meal.TotalWeight.ToString();
            WeightEntry.Text = WeightEntry.Placeholder;


            Calories = meal.CaloriesInMeal;
            Proteins = meal.ProteinsInMeal;
            Fats = meal.FatsInMeal;
            Carbs = meal.CarbsInMeal;

            MealsPicker.SelectedItem = MealTime;

            MealsPicker.ItemsSource = Meals;
        }

        private async void Put_Clicked(object sender, EventArgs e)
        {
            TimeSpan _time = timePicker.Time;
            Time += _time;

            Weight = double.Parse(WeightEntry.Text);

            ration = new RationBindingModel
            {
                CaloriesInFood = Calories,
                ProteinsInFood = Proteins,
                FatsInFood = Fats,
                CarbsInFood = Carbs,
                Meal = MealTime,
                FoodName = MealName,
                Weight = Weight,
                Time = Time,
                Date = DateTime.Now
            };

            await service.PutFoodInRationAsync(ration, Settings.AccessToken);

            await Navigation.PushAsync(new MainPageTabbedForm());
        }

        private void MealsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            MealTime = MealsPicker.Items[MealsPicker.SelectedIndex];
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MealsPage());
        }

        private async void MealName_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Info",
                $"Калорийность: {_meal.CaloriesInMeal:0} \n" +
                $"Белки: {_meal.ProteinsInMeal:0} \n" +
                $"Жиры: {_meal.FatsInMeal:0} \n" +
                $"Углеводы: {_meal.CarbsInMeal:0}",
                "OK");
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            FoodsList.IsVisible = false;
            EditMeal_List.IsVisible = true;
            EditMealList_Frame.IsVisible = true;
            FoodsList_Frame.IsVisible = false;
            update_btn.IsVisible = true;

            LoadEditFoods();
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            var model = new MealsBindingModel { MealName = _meal.MealName, Foods = _Foods, Id = _meal.Id };

            await service.UpdateMealAsync(_meal.Id, model, Settings.AccessToken);

            EditMeal_List.IsVisible = false;
            update_btn.IsVisible = false;
            EditMealList_Frame.IsVisible = false;

            FoodsList.IsVisible = true;
            FoodsList_Frame.IsVisible = true;

            var m = await service.GetMealAsync(_meal.Id, Settings.AccessToken);
            WeightEntry.Placeholder = null;
            WeightEntry.Placeholder = m.TotalWeight.ToString();
            WeightEntry.Text = WeightEntry.Placeholder;

            GetMealFoods();
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            Command RemoveCommand = new Command<FoodBindingModel>((food) => Foods.Remove(food));
            var button = sender as Button;
            var food = button.BindingContext as FoodBindingModel;
            RemoveCommand.Execute(food);

            await service.DeleteFoodFromMealAsync(_meal.Id, food.Id, Settings.AccessToken);

            EditMeal_List.ItemsSource = null;
            LoadEditFoods();
        }

        private async void DeleteMeal_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("", "Вы хотите удалить блюдо?", "Да", "Отмена");
            if (answer == true)
            {
                await service.DeleteMealFromDbAsync(_meal.Id, Settings.AccessToken);
                await Navigation.PushAsync(new MealsPage());
            }
            
        }


        private async void GetMealFoods()
        {
            _Foods = await service.GetMealFoodsAsync(_meal.Id, Settings.AccessToken);
            Foods = new ObservableCollection<FoodBindingModel>(_Foods);

            FoodsList.ItemsSource = Foods;
            FoodsList.HeightRequest = Foods.Count * 48;
            FoodsList_Frame.HeightRequest = Foods.Count * 48;

            // foreach (var f in Foods) { GetPropertyValues(f); }
        }

        private async void LoadEditFoods()
        {
            _Foods = await service.GetMealFoodsAsync(_meal.Id, Settings.AccessToken);
            Foods = new ObservableCollection<FoodBindingModel>(_Foods);

            EditMeal_List.ItemsSource = Foods;
            EditMeal_List.HeightRequest = Foods.Count * 48;
            EditMealList_Frame.HeightRequest = Foods.Count * 48;
        }

        
        //private static void GetPropertyValues(object obj)
        //{
        //    Type t = obj.GetType();
        //    PropertyInfo[] props = t.GetProperties();
        //    foreach (var prop in props)
        //        if (prop.GetIndexParameters().Length == 0)
        //            Debug.WriteLine(prop.GetValue(obj));
        //        else
        //            Debug.WriteLine("   {0} ({1}): <Indexed>", prop.Name,
        //                              prop.PropertyType.Name);
        //}
    }
}
