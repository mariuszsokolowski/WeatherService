using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Data.Models;

namespace WeatherService.Data.Services.WeatherServices
{
    public interface IWeatherService
    {
        /// <summary>
        /// Get data from third-part weather API
        /// </summary>
        public Task<ForecastData> GetAsync(WeatherApiRequestModel city);
    }
}
