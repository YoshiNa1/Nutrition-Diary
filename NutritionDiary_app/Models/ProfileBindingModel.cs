using System.ComponentModel;
using Newtonsoft.Json;
using NutritionDiary_app.Helpers;
using Xamarin.Forms.Internals;

namespace NutritionDiary_app.Models
{
    /// <summary>
    /// Model for health profile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ProfileBindingModel : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// The declaration of the property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Property

        ///// <summary>
        ///// Gets or sets the property that has been displays the category.
        ///// </summary>
        //public string Category { get; set; }

        ///// <summary>
        ///// Gets or sets the property that has been displays the category value.
        ///// </summary>
        //public string CategoryValue { get; set; }

        ///// <summary>
        ///// Gets or sets the property that has been displays the category image.
        ///// </summary>
        //public string ImagePath { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Gender")]
        public string Gender { get; set; }

        [JsonProperty("Age")]
        public int Age { get; set; }

        [JsonProperty("Weight")]
        public double Weight { get; set; }

        [JsonProperty("Height")]
        public double Height { get; set; }

        [JsonProperty("ActivityLevel")]
        public double ActivityLevel { get; set; }

        [JsonProperty("Goal")]
        public double Goal { get; set; }



        [JsonProperty("Metabolism")]
        public double Metabolism { get; set; }

        [JsonProperty("BMI")]
        public double BMI { get; set; }

        [JsonProperty("Result")]
        public double Result { get; set; }


        [JsonProperty("Proteins")]
        public double Proteins { get; set; }

        [JsonProperty("Fats")]
        public double Fats { get; set; }

        [JsonProperty("Carbs")]
        public double Carbs { get; set; }


        [JsonProperty("UserId")]
        public string UserId { get; set; }

        #endregion


        #region Methods

        //public override bool Equals(object obj)
        //{
        //    HealthProfile profile = obj as HealthProfile;
        //    return Id == profile.Id;
        //}

        /// <summary>
        /// The PropertyChanged event occurs when changing the value of property.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected void OnPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
