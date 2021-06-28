using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using NutritionDiary_app.Helpers;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using Xamarin.Forms;

namespace NutritionDiary_app.ViewModels
{
    public class AddMealInDbViewModel
    {
        private readonly ApiService service = new ApiService();

        public string MealName { get; set; }
        public ObservableCollection<FoodBindingModel> Foods { get; set; }
        public string FoodName { get; set; }
        public double Weight { get; set; }

        Command<SelectedItemChangedEventArgs> _foodSelectedCommand;
        public Command<SelectedItemChangedEventArgs> FoodSelectedCommand => _foodSelectedCommand ??
        (_foodSelectedCommand = new Command<SelectedItemChangedEventArgs>(ExecuteListViewItemSelectedCommand));

        public AddMealInDbViewModel()
        {
            Foods = new ObservableCollection<FoodBindingModel>();
        }

        public void ExecuteListViewItemSelectedCommand(SelectedItemChangedEventArgs e)
        {
            var food = e.SelectedItem as FoodBindingModel;

            Foods.Add(food);
        }

        
        public Command AddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //foreach (var f in Foods)
                    //{
                    //    GetPropertyValues(f);
                    //}

                    var list = new List<FoodBindingModel>(Foods);

                    var model = new MealsBindingModel { MealName = MealName, Foods = list };

                    await service.AddMealInDbAsync(model, Settings.AccessToken);
                });
            }
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
