using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class MasterPage : MasterDetailPage
    {
		      
        public MasterPage()
        {
            InitializeComponent();
           
			App.NavPage = new NavigationPage(new CustomTabPage()) { BarBackgroundColor = Color.FromHex("#23144B") };

			Detail = App.NavPage; //new NavigationPage (new TabPage()){ BarBackgroundColor = Color.FromHex("#23144B")};
                    
        }
    }
}
