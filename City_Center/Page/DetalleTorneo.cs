using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class DetalleTorneo : ContentPage
    {
        public DetalleTorneo()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (SLR.IsVisible == false)
            {
                SLR.IsVisible = true;
                SLT.IsVisible = false;
            }
            else
            {
                SLR.IsVisible = false;
                SLT.IsVisible = true;
            }
           
        }
    }
}
