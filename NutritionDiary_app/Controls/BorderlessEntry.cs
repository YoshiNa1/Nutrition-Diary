using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NutritionDiary_app.Controls
{
    /// <summary>
    /// This class is inherited from Xamarin.Forms.Entry to remove the border for Entry control in the Android platform.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class BorderlessEntry : Entry
    {
        public static implicit operator BorderlessEntry(string v)
        {
            throw new NotImplementedException();
        }
    }
}