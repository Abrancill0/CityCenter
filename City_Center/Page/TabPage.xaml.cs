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


		protected override void OnAppearing()
        {
            base.OnAppearing();
            this.CurrentPageChanged += MainPage_CurrentPageChanged;
            ChangeBarColor();
        }

        protected override void OnDisappearing()
        {
            this.CurrentPageChanged -= MainPage_CurrentPageChanged;
            base.OnDisappearing();
        }

        private void MainPage_CurrentPageChanged(object sender, EventArgs e)
        {
			try
			{
				ChangeBarColor();
			}
			catch (Exception)
			{

			}

        }

        private void ChangeBarColor()
        {
			try
			{
				var currentPage = this.CurrentPage;
                switch (currentPage.Icon)
                {
					case "home":

						if (Device.OS == TargetPlatform.iOS)
						{
							this.BarBackgroundColor = Color.FromHex("#645877");
	
						}
                        else
						{
							this.BackgroundColor = Color.FromHex("#645877");
						}

                        
                        
                        break;
                        
					case "casino":

						if (Device.OS == TargetPlatform.iOS)
                        {
							this.BarBackgroundColor = Color.FromHex("#ec3f79");

                        }
                        else

                        {
							this.BackgroundColor = Color.FromHex("#ec3f79");
                        }
                        
						App.NavPage.BarBackgroundColor= Color.FromHex("#E91E63");
						                  
						break;

					case "hotel":

						if (Device.OS == TargetPlatform.iOS)
                        {
							this.BarBackgroundColor = Color.FromHex("#57acb4");

                        }
                        else

                        {
							this.BackgroundColor = Color.FromHex("#57acb4");
                        }
                  
						App.NavPage.BarBackgroundColor = Color.FromHex("#2D97A3");

                        break;

					case "food":
						
						if (Device.OS == TargetPlatform.iOS)
                        {
							this.BarBackgroundColor = Color.FromHex("#fd693a");

                        }
                        else

                        {
							this.BackgroundColor = Color.FromHex("#fd693a");
                        }

						App.NavPage.BarBackgroundColor = Color.FromHex("#FF5722");

						break;

					case "info2":
						if (Device.OS == TargetPlatform.iOS)
                        {
							this.BarBackgroundColor = Color.FromHex("#6573c4");

                        }
                        else

                        {
							this.BackgroundColor = Color.FromHex("#6573c4");
                        }

						App.NavPage.BarBackgroundColor = Color.FromHex("#3F51B5");

                        break;
                }
			}
			catch (Exception ex)
			{

			}

        }

    }
}
