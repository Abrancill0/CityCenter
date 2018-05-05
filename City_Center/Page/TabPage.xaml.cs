using System;
using Naxam.Controls.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace City_Center.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabPage : BottomTabbedPage
    {
        public TabPage()
        {
            InitializeComponent();

            NavigationPage.SetTitleIcon(this, "logo.png");
        }
    }
}
