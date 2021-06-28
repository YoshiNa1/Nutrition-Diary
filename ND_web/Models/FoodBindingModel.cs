using System;
using System.Collections.Generic;

namespace ND_web.Models
{
    public class FoodBindingModel
    {
        public int Id { get; set; }

        public string FoodName { get; set; }

        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbs { get; set; }

        public double CaloriesConsumed { get; set; }
        public double ProteinsConsumed { get; set; }
        public double FatsConsumed { get; set; }
        public double CarbsConsumed { get; set; }

        public double Weight { get; set; }

        public int MealsBindingModelId { get; set; }
    }
}
