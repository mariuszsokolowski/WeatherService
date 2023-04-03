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
        private const string _baseURL = "https://api.openweathermap.org/data/2.5/weather";
        private const string _api_key = "c84e3db78e3d22d53163b6021fd3797c";
        private readonly HttpClient _httpClient;
        private readonly ICityService _cityService;

        public WeatherService(ICityService cityService)
        {
            _cityService = cityService;
            _httpClient = new HttpClient();
        }
        public async Task<WeatherApiResponseModel> Get(WeatherApiRequestModel weatherApiRequestModel)
        {
            try
            {
                var url = $"{_baseURL}?q={weatherApiRequestModel.City}&appid={_api_key}&units={weatherApiRequestModel.TempType}";
                var response = await _httpClient.GetFromJsonAsync<WeatherApiResponseModel>(url);

                CityModel cityModel = new CityModel()
                {
                    Name = response.Name,
                    CountryCode = response.Sys.Country
                };

                await _cityService.AddAsync(cityModel);
                return response;

            }
            catch (Exception e)
            {
                var test = e.Message;
            }
            return null;
        }
    }
}
