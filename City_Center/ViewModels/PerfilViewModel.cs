using System;
using Xamarin.Forms;

namespace City_Center.ViewModels
{
    public class PerfilViewModel:BaseViewModel
    {
        #region Services

        #endregion

        #region Attributes
        private string email;
        private string contraseña;
        private string contraseña2;
        private string ciudad;
        private string fecha;

        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Contraseña
        {
            get { return this.contraseña; }
            set { SetValue(ref this.contraseña, value); }
        }

        public string Contraseña2
        {
            get { return this.contraseña2; }
            set { SetValue(ref this.contraseña2, value); }
        }

        public string Ciudad
        {
            get { return this.ciudad; }
            set { SetValue(ref this.ciudad, value); }
        }

        public string Fecha
        {
            get { return this.fecha; }
            set { SetValue(ref this.fecha, value); }
        }



        #endregion

        #region Commands

        #endregion

        #region Methods
        private void LoadEventos()
        {
            Email = Application.Current.Properties["Email"].ToString();
            //Application.Current.Properties["NombreCompleto"]
            Ciudad = Application.Current.Properties["Ciudad"].ToString();
            Contraseña = Application.Current.Properties["Pass"].ToString();
            Contraseña2 = Application.Current.Properties["Pass"].ToString();
            Fecha = Application.Current.Properties["FechaNacimiento"].ToString();

        }
        #endregion

        #region Contructors
        public PerfilViewModel()
        {
            LoadEventos();
        }
        #endregion
    }

}
