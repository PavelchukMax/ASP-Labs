using Microsoft.AspNetCore.Mvc;
using lab_11.Services.WeatherService;
using lab_11.Models;

namespace lab_11.ViewComponents
{
    public class WeatherViewComponent : ViewComponent
    {
        private readonly IWeatherService _weatherService;

        public WeatherViewComponent(IWeatherService myService)
        {
            _weatherService = myService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Coord coord)
        {
            WeatherData weather = await _weatherService.GetWeatherAsync(coord.lat, coord.lon);
            return View(weather);
        }

    }
}
