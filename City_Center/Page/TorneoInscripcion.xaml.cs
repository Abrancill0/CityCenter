using System;
using System.Collections.Generic;
using System.Net.Http;
using City_Center.Clases;
using City_Center.Models;
using Xamarin.Forms;

namespace City_Center.Page
{
    public partial class TorneoInscripcion : ContentPage
    {
		private string[] ListaOpciones;

        public TorneoInscripcion()
        {
            InitializeComponent();

			ListaOpciones = new string[] { "DNI", "LE", "LC", "CI" };

            TipoDocumento.ItemsSource = ListaOpciones;

            TipoDocumento.SelectedIndex = 0;
        }

		async void  Handle_Clicked(object sender, System.EventArgs e)
		{
			if (string.IsNullOrEmpty(Nombre.Text))
            {
                await Mensajes.Alerta("Nombre Requerido");
                return;
            }

			if (string.IsNullOrEmpty(Correo.Text))
            {
                await Mensajes.Alerta("Correo Requerido");
                return;
            }

			if (string.IsNullOrEmpty(NumeroDocumento.Text))
            {
                await Mensajes.Alerta("Numero de documento Requerido");
                return;
            }

			if (string.IsNullOrEmpty(Nacionalidad.Text))
            {
                await Mensajes.Alerta("Nacionalidad Requerido");
                return;
            }

			if (string.IsNullOrEmpty(Provincia.Text))
            {
                await Mensajes.Alerta("Provincia Requerido");
                return;
            }

                    
			if (string.IsNullOrEmpty(Pais.Text))
            {
                await Mensajes.Alerta("Pais Requerido");
                return;
            }

			if (string.IsNullOrEmpty(Ciudad.Text))
            {
                await Mensajes.Alerta("Ciudad Requerido");
                return;
            }

            var content = new FormUrlEncodedContent(new[]
            {
				new KeyValuePair<string, string>("email", Correo.Text),
                new KeyValuePair<string, string>("tor_nombre", ""),
				new KeyValuePair<string, string>("nombre", Convert.ToString(Nombre.Text)),
				new KeyValuePair<string, string>("numero_documento", NumeroDocumento.Text),
				new KeyValuePair<string, string>("nacionalidad", Nacionalidad.Text),
				new KeyValuePair<string, string>("provincia", Provincia.Text),
                new KeyValuePair<string, string>("tipo_de_documento", TipoDocumento.SelectedItem.ToString()),
                new KeyValuePair<string, string>("fecha_nac", Convert.ToString(Fecha)),
				new KeyValuePair<string, string>("pais", Pais.Text),
				new KeyValuePair<string, string>("ciudad", Ciudad.Text)

            });


			Restcliente DatosUsuarioRequest = new Restcliente();

			var response = await DatosUsuarioRequest.Get<GuardadoGenerico>("/casino/torneos/registro_torneo", content);
         
            if (response.estatus == 0 )
            {
                await Mensajes.Alerta("Error al enviar el correo");
                return;
            }

            await Mensajes.Alerta("Correo enviado exitosamente");


			Correo.Text = string.Empty;
			Nombre.Text=string.Empty;         
			NumeroDocumento.Text=string.Empty;
			Nacionalidad.Text=string.Empty;
			Provincia.Text = string.Empty;
			Ciudad.Text =string.Empty;
            
		}


        async void Chat_click(object sender, System.EventArgs e)
        {
            bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ?
                                    (bool)Application.Current.Properties["IsLoggedIn"] : false;

            if (isLoggedIn)
            {
                #if __ANDROID__
                await ((MasterPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new SeleccionTipoChat());
#endif
            }
            else
            {
                await Mensajes.Alerta("Es necesario que te registres para completar esta acción");
            }
        }
    }
}
