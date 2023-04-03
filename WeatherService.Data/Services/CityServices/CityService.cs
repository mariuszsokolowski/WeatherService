using AutoMapper;
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
        private readonly IMapper _mapper;
        public CityService(DBContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<CityModel> AddAsync(CityModel model)
        {
            try
            {
                //Map CityModel to City
                var city =_mapper.Map<Entities.City>(model);

                //If city dosen't exist add to DB
                if (!_context.City.Where(x => x.Name.ToLower() == city.Name.ToLower() && x.CountryCode.ToLower() == city.CountryCode.ToLower()).Any())
                {
                    //Add new City to DB
                    await _context.City.AddAsync(city);
                    await _context.SaveChangesAsync();
                }
            }
            // If catch -> will be validate by MSSQL index
            catch 
            {
            }
            return model;
        }

        public async Task<List<CityModel>> GetAsync()
        {
            //Get cities from DB
            var cities = await _context.City.ToListAsync();
            //Map List<City> to List<CityModel>
            var result = _mapper.Map<List<CityModel>>(cities);
            return result;
        }
    }
}
