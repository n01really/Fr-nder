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
        // API-nyckel för att autentisera mot vädertjänsten
        private const string ApiKey = "jHCKnpEHjGnuo5Kl1qp8Mg==Kf2RJYQrlX0Nc4yE";
        // Bas-URL för vädertjänstens API
        private const string BaseUrl = "https://api.api-ninjas.com/v1/weather";

        private readonly HttpClient _httpClient;

        // Konstruktor som initialiserar HttpClient och lägger till API-nyckeln i headers
        public WeatherService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
        }

        // Hämtar väderdata baserat på latitud och longitud
        public async Task<Weather> GetWeatherByCoordinatesAsync(double latitude, double longitude)
        {
            var url = $"{BaseUrl}?lat={latitude}&lon={longitude}";

            try
            {
                // Skickar en GET-förfrågan till API:et
                var response = await _httpClient.GetAsync(url);

                // Kontrollera om förfrågan lyckades
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Fel vid hämtning av data: {response.ReasonPhrase}");
                }

                // Läser och deserialiserar JSON-svaret till en Weather-objekt
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Weather>(json);
            }
            catch (Exception ex)
            {
                // Loggar eventuella fel
                Console.WriteLine($"Fel: {ex.Message}");
                return null;
            }
        }
    }

}
