using MobileWeather.Helpers;
using MobileWeather.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MobileWeather.Services
{
    public class APIService : IAPIService
    {
        public async Task<ChangeUser> GetUserAsync(string username)
        {
            var client = new HttpClient();

            var response = await client.GetAsync("https://imobilar.azurewebsites.net/api/modifyuser" + $"?username={username}");

            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();

                var changedUser = JsonConvert.DeserializeObject<ChangeUser>(contentString);

                if (changedUser == null)
                {
                    return null;
                }
                else
                {
                    return changedUser;
                }
            }

            return null;
        }

        public async Task<Response> GetWeather<T>(string location)
        {

            try
            {
                var url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid=c193e953d32a4717cf56ab697cc3c10f&lang={1}", location, Languages.Language);
                var client = new HttpClient();
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var weather = JsonConvert.DeserializeObject<WeatherResponse>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = weather
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var client = new HttpClient();

            var loginBody = new UserLogin
            {
                Password = password,
                Username = username
            };

            var json = JsonConvert.SerializeObject(loginBody);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://imobilar.azurewebsites.net/api/users", content);

            return response.IsSuccessStatusCode;


        }

        public async Task<ChangeUser> UserChange(string firstName, string lastName, string address, int fiscalNumber, int citizenCardNumber, int age)
        {
            string savedUsername = string.Empty;
            var properties = Xamarin.Forms.Application.Current.Properties;

            if (properties.ContainsKey("username"))
            {
                savedUsername = (string)properties["username"];
            }

            var client = new HttpClient();

            var model = new ChangeUser
            {
                FirstName = firstName,
                LastName = lastName,
                Username = savedUsername,
                Address = address,
                FiscalNumber = fiscalNumber,
                CitizenCardNumber = citizenCardNumber,
                Age = age,
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync("https://imobilar.azurewebsites.net/api/modifyuser", content);

            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();

                var changedUser = JsonConvert.DeserializeObject<ChangeUser>(contentString);

                if (changedUser == null)
                {
                    return null;
                }
                else
                {
                    return changedUser;
                }
            }

            return null;
        }
    }
}
