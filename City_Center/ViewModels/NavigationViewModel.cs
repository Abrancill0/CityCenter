using System;

using System.Drawing;

namespace City_Center.ViewModels
{
	public class NavigationViewModel : BaseViewModel
	{
		Color barBackgroundColor = Color.Transparent;
		public Color BarBackgroundColor
		{
			get => barBackgroundColor;
			set => SetValue(ref barBackgroundColor, value);
		}
	}
}