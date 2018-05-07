using Acr.UserDialogs;
using System;
using System.Threading.Tasks;

namespace City_Center.Clases
{                    
    class Mensajes
    {
    public static async Task<IDisposable> Error(string Mensaje)
    {
        ToastConfig.DefaultBackgroundColor = System.Drawing.Color.FromArgb(244, 47, 73);
        var mensajeError = UserDialogs.Instance.Toast(new ToastConfig(Mensaje).SetIcon("error.png"));

        return mensajeError;
    }

    public static async Task<IDisposable> Info(string Mensaje)
    {
        ToastConfig.DefaultBackgroundColor = System.Drawing.Color.FromArgb(109, 188, 219);
        var mensajeError = UserDialogs.Instance.Toast(new ToastConfig(Mensaje).SetIcon("informacion.png"));

        return mensajeError;
    }

    public static async Task<IDisposable> success(string Mensaje)
    {

        ToastConfig.DefaultBackgroundColor = System.Drawing.Color.FromArgb(56, 166, 84);
        var mensajeError = UserDialogs.Instance.Toast(new ToastConfig(Mensaje).SetIcon("success.png"));

        return mensajeError;
    }

    //public static async Task<IDisposable> warning(string Mensaje)
   
    }
}

