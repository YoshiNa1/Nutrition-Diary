using System;
using System.Collections.Generic;

namespace ND_web.Models
{
    public class MealsBindingModel
    {
        public int Id { get; set; }

        public string MealName { get; set; }

        public double CaloriesInMeal { get; set; }
        public double ProteinsInMeal { get; set; }
        public double FatsInMeal { get; set; }
        public double CarbsInMeal { get; set; }

        public double TotalWeight { get; set; }

        public List<FoodBindingModel> Foods { get; set; }

        public string UserId { get; set; }

    }
}
