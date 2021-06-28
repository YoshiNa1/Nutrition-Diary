using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NutritionDiary_app.Models;
using NutritionDiary_app.Services;
using Xamarin.Forms;

namespace NutritionDiary_app.ViewModels
{
    public class PutMealInRationViewModel : BaseViewModel
    {
        private readonly ApiService service = new ApiService();

        //public string MealName { get; set; }
        public ObservableCollection<FoodBindingModel> Foods { get; set; }
        //public string FoodName { get; set; }
        //public double Weight { get; set; }

        public PutMealInRationViewModel()
        {
            Foods = new ObservableCollection<FoodBindingModel>();
        }
    }
}
