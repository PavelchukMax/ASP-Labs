using lab_11.Models;

namespace lab_11.Services.WeatherService
{
    public class WeatherService : IWeatherService
    {
        private IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<WeatherData> GetWeatherAsync(double latitude, double longitude)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid=b281fdd26c696cbccf7d09f3889dcc39");
                await Console.Out.WriteLineAsync($"fetched data : {response}");
                if (response.IsSuccessStatusCode)
                {
                    var weatherData = await response.Content.ReadFromJsonAsync<WeatherData>();
                    return weatherData;
                }
                throw new HttpRequestException();
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(double latitude, double longitude);
    }
}
