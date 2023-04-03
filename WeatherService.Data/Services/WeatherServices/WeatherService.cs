using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Data.Models;
using WeatherService.Data.Services.CityServices;

namespace WeatherService.Data.Services.WeatherServices
{
    public class WeatherService : IWeatherService
    {
        private const string _baseURL = "https://api.openweathermap.org/data/2.5/forecast";
        private const string _api_key = "c84e3db78e3d22d53163b6021fd3797c";
        private readonly HttpClient _httpClient;
        private readonly ICityService _cityService;

        public WeatherService(ICityService cityService)
        {
            _cityService = cityService;
            _httpClient = new HttpClient();
        }
        public async Task<ForecastData> GetAsync(WeatherApiRequestModel weatherApiRequestModel)
        {
                if(weatherApiRequestModel== null || weatherApiRequestModel.Cnt<=0 || string.IsNullOrEmpty(weatherApiRequestModel.City))
                {
                    throw new Exception("Parametrs cannot be null");
                }
                //Set URL
                var url = $"{_baseURL}?q={weatherApiRequestModel.City}&appid={_api_key}&cnt={weatherApiRequestModel.Cnt}&units={weatherApiRequestModel.TempType}";
                var response = await _httpClient.GetFromJsonAsync<ForecastData>(url);
                await _cityService.AddAsync(new CityModel() { CountryCode = response.City.Country, Name = response.City.Name });
                return response;
        }
    }
}
