package crc645548ddd2ac4f57bc;


public class CalenderDatePickerRenderer
	extends crc643f46942d9dd1fff9.DatePickerRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("NutritionDiary_app.Droid.CalenderDatePickerRenderer, NutritionDiary_app.Android", CalenderDatePickerRenderer.class, __md_methods);
	}


	public CalenderDatePickerRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CalenderDatePickerRenderer.class)
			mono.android.TypeManager.Activate ("NutritionDiary_app.Droid.CalenderDatePickerRenderer, NutritionDiary_app.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public CalenderDatePickerRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CalenderDatePickerRenderer.class)
			mono.android.TypeManager.Activate ("NutritionDiary_app.Droid.CalenderDatePickerRenderer, NutritionDiary_app.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CalenderDatePickerRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CalenderDatePickerRenderer.class)
			mono.android.TypeManager.Activate ("NutritionDiary_app.Droid.CalenderDatePickerRenderer, NutritionDiary_app.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
