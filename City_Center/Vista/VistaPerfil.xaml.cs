using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace City_Center.Vista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VistaPerfil : ContentView
	{
		public VistaPerfil ()
		{
			InitializeComponent ();

            try
            {
                TextCorreoProvisional.Text = App.Current.Properties["Email"].ToString();
                TextPassProvisional1.Text = App.Current.Properties["Pass"].ToString();
                TextPassProvisional2.Text = App.Current.Properties["Pass"].ToString();
                TextCiudadProvisional1.Text = App.Current.Properties["Ciudad"].ToString();
                TextFechaProvisional1.Text = App.Current.Properties["FechaNacimiento"].ToString();
            }
            catch (Exception ex)
            {

            }
           
		}
	}
}