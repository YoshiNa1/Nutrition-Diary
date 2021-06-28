using Xamarin.Forms.Internals;

namespace NutritionDiary_app.Models
{
    /// <summary>
    /// Model for daily calories report page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class Nutrient
    {
        #region Property


        public double Quantity { get; set; }

        public string Name { get; set; }

        public string Indicator { get; set; }

        public Nutrient()
        {

        }
        public Nutrient(double quantity, string name)
        {
            Quantity = quantity; Name = name;
        }

        #endregion
    }
}
