using City_Center.Controls;
using City_Center.iOS.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace City_Center.iOS.Controls
{
    public class TLScrollViewRenderer : Xamarin.Forms.Platform.iOS.ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var element = e.NewElement as TLScrollView;
            element?.Render();
        }
    }
}