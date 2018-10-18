using System;
using System.ComponentModel;

namespace City_Center.Clases
{
    public class GlobalResources : INotifyPropertyChanged
    {
        // Singleton
        public static GlobalResources Current = new GlobalResources();

        private string _imagenChat;
        private string _imagenPerfil;
        private string _imagenTarjeta;

        public string ImagenChat
        {
            get { return _imagenChat; }
            set
            {
                _imagenChat = value;
                OnPropertyChanged("ImagenChat");
            }
        }

        public string ImagenPerfil
        {
            get { return _imagenPerfil; }
            set
            {
                _imagenPerfil = value;
                OnPropertyChanged("ImagenPerfil");
            }
        }

        public string ImagenTarjeta
        {
            get { return _imagenTarjeta; }
            set
            {
                _imagenTarjeta = value;
                OnPropertyChanged("ImagenTarjeta");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
