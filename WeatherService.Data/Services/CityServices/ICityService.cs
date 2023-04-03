using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Data.Models;

namespace WeatherService.Data.Services.CityServices
{
    public interface ICityService
    {

        /// <summary>
        /// Get all cities from DB
        /// </summary>
        /// <returns></returns>
        public Task<List<CityModel>> GetAsync();

        /// <summary>
        /// Add city to DB if not exist
        /// </summary>
        /// <returns></returns>
        public Task<CityModel> AddAsync(CityModel model);

    }
}
