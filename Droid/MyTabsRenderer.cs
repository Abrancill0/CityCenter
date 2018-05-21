using Android.OS;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using System.ComponentModel;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Content.Res;
using City_Center.Droid;
using City_Center;
using Naxam.Controls.Forms;

[assembly: ExportRenderer(typeof(MyTabs), typeof(MyTabsRenderer))]
namespace City_Center.Droid
{
    public class MyTabsRenderer : TabbedPageRenderer
    {
        bool setup;
        ViewPager pager;
        TabLayout layout;
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (setup)
                return;

            if (e.PropertyName == "Renderer")
            {
                pager = (ViewPager)ViewGroup.GetChildAt(0);
                layout = (TabLayout)ViewGroup.GetChildAt(1);
                setup = true;

                ColorStateList colors = null;
                if ((int)Build.VERSION.SdkInt >= 23)
                {
                    colors = Resources.GetColorStateList(Resource.Color.icon_tab, Forms.Context.Theme);
                }
                else
                {
                    colors = Resources.GetColorStateList(Resource.Color.icon_tab);
                }

                for (int i = 0; i < layout.TabCount; i++)
                {
                    var tab = layout.GetTabAt(i);
                    var icon = tab.Icon;
                    if (icon != null)
                    {
                        icon = Android.Support.V4.Graphics.Drawable.DrawableCompat.Wrap(icon);
                        Android.Support.V4.Graphics.Drawable.DrawableCompat.SetTintList(icon, colors);
                    }
                }

            }
        }
    }
}
