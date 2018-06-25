using System;
using System.ComponentModel;

namespace City_Center.Clases
{
    public class GlobalResources : INotifyPropertyChanged
    {
        // Singleton
        public static GlobalResources Current = new GlobalResources();

        private string _imagenChat;

        public string ImagenChat
        {
            get { return _imagenChat; }
            set
            {
                _imagenChat = value;
                OnPropertyChanged("ImagenChat");
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
