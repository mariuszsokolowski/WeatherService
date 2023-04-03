using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherService.Data;
using WeatherService.Data.Models;
using WeatherService.Data.Services.CityServices;
using WeatherService.Data.Services.WeatherServices;
using WeatherService.Web.Models;

namespace WeatherService.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly ICityService _cityService;

        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService, ICityService cityService)
        {
            _logger = logger;
            _weatherService = weatherService;
            _cityService = cityService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Cities = await _cityService.GetAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FetchWeatherData(WeatherApiRequestModel weatherApiRequestModel)
        {
            try
            {
                if (string.IsNullOrEmpty(weatherApiRequestModel.City))
                {
                    return BadRequest("City cannot be null or empty");
                }
                var result = await _weatherService.GetAsync(weatherApiRequestModel);
                return PartialView("_WeatherData", result);
            }
            catch (Exception e)
            { return BadRequest(e.Message); }
        }

    }
}