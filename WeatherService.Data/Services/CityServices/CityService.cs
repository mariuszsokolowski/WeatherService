using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Data.Entities;
using WeatherService.Data.Models;

namespace WeatherService.Data.Services.CityServices
{
    public class CityService : ICityService
    {
        private readonly DBContext _context;
        public CityService(DBContext context)
        {
            _context = context;
        }
        public async Task<CityModel> AddAsync(CityModel model)
        {
            try
            {
                City city = new City()
                {
                    CountryCode = model.CountryCode,
                    Name = model.Name
                };
                await _context.City.AddAsync(city);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var msg = e.Message;
            }
            return model;
        }

        /// <summary>
        /// Get all cities from DB
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<CityModel>> GetAsync()
        {
            var cities = await _context.City.ToListAsync();
            List<CityModel> result = new List<CityModel>();
            foreach (var item in cities)
            {
                result.Add(new CityModel { CountryCode = item.CountryCode, Name = item.Name });
            }
            return result;
        }
    }
}
