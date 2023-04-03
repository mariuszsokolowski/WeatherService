using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherService.Data.Entities;
using WeatherService.Data.Models;

namespace WeatherService.Data.Automapper
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, CityModel>();
            CreateMap<CityModel, Entities.City>();

        }
    }
}
