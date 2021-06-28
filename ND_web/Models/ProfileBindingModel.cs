using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ND_web.Models
{
    public class ProfileBindingModel
    { 
        /// <summary>
        /// Gets or sets the profile id.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Gets or sets the profile sex.
        /// </summary>
        [Required(ErrorMessage = "Выберите пол, пожалуйста")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        [Required(ErrorMessage = "Введите возраст, пожалуйста")]
        [Range(1, 100, ErrorMessage = "Введите корректный возраст, пожалуйста")]
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        [Required(ErrorMessage = "Установите вес, пожалуйста")]
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [Required(ErrorMessage = "Установите рост, пожалуйста")]
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the activity level.
        /// </summary>
        [Required(ErrorMessage = "Выберите уровень активности, пожалуйста")]
        public double ActivityLevel { get; set; }

        /// <summary>
        /// Gets or sets the goal.
        /// </summary>
        [Required(ErrorMessage = "Выберите цель, пожалуйста")]
        public double Goal { get; set; }


        public double Metabolism { get; set; }
        public double BMI { get; set; }
        public double Result { get; set; }

        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbs { get; set; }

        public string UserId { get; set; }
    }
}
