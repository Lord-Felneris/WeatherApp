using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeatherApp.Models.Model;

namespace WeatherApp.Services.Service
{
    public class AccuWeatherService
    {
        //defining a base url / endpoint / getapikey
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITION_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        public static string API_KEY = Settings.GetApiKey;

        // get list of city : need query write by user
        public  async static Task<List<City>> GetCities(string query)
        {
            //store a list of city from request
            List<City>? cities = new List<City>();
            //url for request
            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);
            //make a request
            using (HttpClient client = new HttpClient()) 
            {
                //1.set variable for response
                var response = await client.GetAsync(url);
                //2.get a json from response
                string json = await response.Content.ReadAsStringAsync();
                //3.deserialize a json
                cities = JsonConvert.DeserializeObject<List<City>>(json);
                //4. don't forget to set a method  a task because a method is async
            }
            return cities;
        }
        
        //get a current contdition
        public async static Task<CurrentCondition> GetCurrentCondition(string cityKey)
        {
            //store a city condition
            CurrentCondition? cityCurrentCondition = new CurrentCondition();
            //url for request
            string url = BASE_URL + string.Format(CURRENT_CONDITION_ENDPOINT, cityKey, API_KEY);
            //make a request
            using (HttpClient client = new HttpClient())
            {
                //1.set variable for response
                var response = await client.GetAsync(url);
                //2.get a json from response
                string json = await response.Content.ReadAsStringAsync();
                //3.deserialize a json
                cityCurrentCondition = (JsonConvert.DeserializeObject<List<CurrentCondition>>(json)).FirstOrDefault();
                //4. don't forget to set a method  a task because a method is async
            }
            return cityCurrentCondition;
        }
    }
}
