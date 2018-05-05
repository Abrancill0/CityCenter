using System;
using System.Collections.Generic;
using Naxam.Controls.Forms;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            BackgroundColor = Color.FromHex("#23144B");

            NavigationPage.SetTitleIcon(this, "logo.png");
            Detail = new NavigationPage (new TabPage());
        }
    }
}
