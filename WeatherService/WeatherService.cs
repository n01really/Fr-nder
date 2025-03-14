using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Fränder.Models;

namespace Fränder.WeatherService
{
    class WeatherService
    {
        private const string ApiKey = "jHCKnpEHjGnuo5Kl1qp8Mg==Kf2RJYQrlX0Nc4yE"; 
        private const string BaseUrl = "https://api.api-ninjas.com/v1/weather";

        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
        }

        public async Task<Weather> GetWeatherByCoordinatesAsync(double latitude, double longitude)
        {
            var url = $"{BaseUrl}?lat={latitude}&lon={longitude}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Fel vid hämtning av data: {response.ReasonPhrase}");
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Weather>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
                return null;
            }
        }
    }

}
