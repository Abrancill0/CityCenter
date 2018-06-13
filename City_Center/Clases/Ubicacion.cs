using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace City_Center.Clases
{
    public class Ubicacion
	{
			public static async Task<Position> GetCurrentPosition()
            {
                Position position = null;
                try
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 100;

				position =  await locator.GetPositionAsync(TimeSpan.FromSeconds(10)); 

                    if (position != null)
                    {
                        //got a cahched position, so let's use it.
                        return position;
                    }

                    if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                    {
                        //not available or enabled
                        return null;
                    }

                    position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

                }
                catch (Exception ex)
                {
				    await Mensajes.Alerta("No se pudo acceder a la ubicacion");
                }

                if (position == null)
                    return null;
            
                return position;
            }
	}
    
}
