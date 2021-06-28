
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(SearchBar), typeof(NutritionDiary_app.iOS.Renderers.ExtendedSearchBarRenderer))]

namespace NutritionDiary_app.iOS.Renderers
{
	public class ExtendedSearchBarRenderer : SearchBarRenderer
	{
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			Control.ShowsCancelButton = false;
			
		}
	}
}