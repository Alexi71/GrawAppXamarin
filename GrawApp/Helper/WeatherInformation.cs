using System;
using System.Collections.Generic;
using GrawApp.Model;
using Newtonsoft.Json;
using RestSharp;

namespace GrawApp.Helper
{
    public class WeatherInformation
    {
        const string WEATHER_URL = "http://api.openweathermap.org/data/2.5/weather";
        const string APP_ID = "e72ca729af228beabd5d20e3b7749713";
        public WeatherInformation()
        {
            
        }

        public static void  GetWeatherData(double latitude, double longitude,Action<WeatherData> action)
        {
            var uri = $"{WEATHER_URL}?lat={latitude}&long={longitude}&appip={APP_ID}";
            var client = new RestClient(WEATHER_URL);

            var request = new RestRequest();
            request.AddQueryParameter("lat", latitude.ToString());
            request.AddQueryParameter("lon", longitude.ToString());
            request.AddQueryParameter("appid", APP_ID);

            //var response = client.Execute(request);
            WeatherData data = new WeatherData();
            client.ExecuteAsync(request, response =>
            {
                Console.WriteLine(response.Content);
                RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
                //dynamic results = JsonConvert.DeserializeObject(response.Content);
                //var results = JsonConvert.DeserializeObject<Dictionary<string,string[]>>(response.Content);

                //  var coord = results["coord"];
                //var lon = coord["lon"];
                //Console.WriteLine($"longitude:{lon.Value}");
                //var main = results["main"][0];


                var jsonObject = deserial.Deserialize<Dictionary<string, string>>(response);
                foreach (var item in jsonObject)
                {
                    Console.WriteLine($"{item.Key} {item.Value}");

                    if (item.Key == "name")
                    {
                        Console.WriteLine($"City: {item.Value}");
                        data.City = item.Value;
                    }

                    if (item.Key == "weather")
                    {
                        var res = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(item.Value);
                        Console.WriteLine($"{res[0]["id"]}");
                        data.Condition = int.Parse(res[0]["id"]);
                    }

                    if (item.Key == "main")
                    {
                        var res = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.Value);
                        Console.WriteLine($"Temperature is: {res["temp"]}");

                        data.Temperature = (res["temp"]).ToDouble() - 273.15;
                    }
                    //Console.WriteLine("Stop");
                }
                //var myObj = jsonObject["main"];

                //Console.WriteLine(jsonObject["main"]);
                data.IconName = data.GetWeatherIcon(data.Condition);
                action.Invoke(data);

            });

        }
    }
}
