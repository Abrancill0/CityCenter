using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace City_Center.Clases
{
    public class Restcliente2
    {

        public async Task<List<T>> Get<T>(string Url, HttpContent Content)
        {

            HttpClient client= new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            // client.MaxResponseContentBufferSize = 1000;

            try
            {
                client.BaseAddress = new Uri("192.168.100.10:83");
                //client.Timeout = TimeSpan.FromSeconds(7);

                HttpResponseMessage response = await client.PostAsync(string.Format(Url), Content);
       
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                 
                    return JsonConvert.DeserializeObject<List<T>>(result);
                    
                }

                client.Dispose();
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                client.CancelPendingRequests();
                client.Dispose();
                return null;

            }


            return null;
        }

    }
}
