using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ND_web.Models
{
    public class RationBindingModel
    {
        public int Id { get; set; }


        public DateTime Date { get; set; }

        public string FoodName { get; set; }

        public double Weight { get; set; }

        public DateTime Time { get; set; }

        public string Meal { get; set; }



        public double CaloriesInFood { get; set; }

        public double ProteinsInFood { get; set; }

        public double FatsInFood { get; set; }

        public double CarbsInFood { get; set; }



        public double CaloriesConsumed { get; set; }

        public double ProteinsConsumed { get; set; }

        public double FatsConsumed { get; set; }

        public double CarbsConsumed { get; set; }

        public string UserId { get; set; }

    }
}
