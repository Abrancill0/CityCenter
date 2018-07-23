using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace City_Center.Clases
{
    public class Restcliente
    {

        public async Task<T> Get<T>(string Url, HttpContent Content)
        {

            HttpClient client= new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
          
            // client.MaxResponseContentBufferSize = 1000;

            try
            {
                client.BaseAddress = new Uri("https://citycenter-rosario.com.ar");
                // client.Timeout = TimeSpan.FromSeconds(7);
                //https://citycenter-rosario.com.ar
                HttpResponseMessage response = await client.PostAsync(string.Format(Url), Content);
               
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    if (result == "mensajes vistos")
                    {
                        VariablesGlobales.MensajeVisto = 1;
                        return JsonConvert.DeserializeObject<T>(result);

                    }
                    else
                    {
                        VariablesGlobales.MensajeVisto = 0;

                        return JsonConvert.DeserializeObject<T>(result);
                       
                    }


                }

                client.Dispose();
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                client.CancelPendingRequests();
                client.Dispose();
                return default(T);

            }


            return default(T);
        }


        public async Task<T> GetReal<T>(string Url, HttpContent Content)
        {

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            // client.MaxResponseContentBufferSize = 1000;

            try
            {
                client.BaseAddress = new Uri("https://citycenter-rosario.com.ar");
                // client.Timeout = TimeSpan.FromSeconds(7);
                //https://citycenter-rosario.com.ar
                HttpResponseMessage response = await client.GetAsync(string.Format(Url));

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    return JsonConvert.DeserializeObject<T>(result);
                }

                client.Dispose();
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                client.CancelPendingRequests();
                client.Dispose();
                return default(T);

            }


            return default(T);
        }


    }
}
