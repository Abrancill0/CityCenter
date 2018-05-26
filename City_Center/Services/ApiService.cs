using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using City_Center.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace City_Center.Services
{
    public class ApiService
    {

        public async Task<Response> CheckConnection()
        {

            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Verifica tu conexión a internet.",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Verifica tu conexión a internet.",
                };

            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };

        }


        public async Task<Response> GetReal<T>(
        string servicePrefix,
        string controller,
        HttpContent Content)
        {
            try
            {

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");


                client.BaseAddress = new Uri("http://cc.comprogapp.com");
                //client.BaseAddress = new Uri("http://192.168.100.10:83");
                var url = string.Format("{0}{1}", servicePrefix, controller);

                HttpResponseMessage response = await client.GetAsync(string.Format(url));

                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var Model = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = Model,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.ToString(),
                };
            }


        }


        public async Task<Response> Get<T>(
            string servicePrefix,
            string controller,
            HttpContent Content)
        {
            try
            {

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                client.BaseAddress = new Uri("http://cc.comprogapp.com");

                var url = string.Format("{0}{1}", servicePrefix, controller);

                HttpResponseMessage response = await client.PostAsync(string.Format(url), Content);

                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

               var Model = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = Model,
                };

            }
            catch (Exception ex)
            {     
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.ToString(),
                };
            }


        }

    }
}
