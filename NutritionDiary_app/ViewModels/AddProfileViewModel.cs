using NutritionDiary_app.Services;
using Xamarin.Forms;
using NutritionDiary_app.Helpers;
using Xamarin.Forms.Internals;

namespace NutritionDiary_app.ViewModels
{
    [Preserve(AllMembers = true)]
    public class AddProfileViewModel : BaseViewModel
    {
        private readonly ApiService service = new ApiService();

        private string gender;
        public string Gender
        {
            get { return gender; }
            set
            {
                if (gender != value)
                {
                    gender = value;
                    NotifyPropertyChanged("Gender");
                }
            }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                if (age != value)
                {
                    age = value;
                    NotifyPropertyChanged("Age");
                }
            }
        }

        private double weight;
        public double Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    NotifyPropertyChanged("Weight");
                }
            }
        }


        private double height;
        public double Height
        {
            get { return height; }
            set
            {
                if (height != value)
                {
                    height = value;
                    NotifyPropertyChanged("Height");
                }
            }
        }

        private double level;
        public double ActivityLevel {
            get { return level; }
            set
            {
                if (level != value)
                {
                    level = value;
                    NotifyPropertyChanged("Level");
                }
            }
        }

        private double goal;
        public double Goal {
            get { return goal; }
            set
            {
                if (goal != value)
                {
                    goal = value;
                    NotifyPropertyChanged("Goal");
                }
            }
        }

        public Command AddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await service.AddProfileAsync
                    (Gender, Age, Weight, Height, ActivityLevel, Goal, Settings.AccessToken);
                });
            }
        }

    }
}