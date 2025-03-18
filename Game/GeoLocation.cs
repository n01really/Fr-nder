using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fränder.Game
{
    class GeoLocation : INotifyPropertyChanged
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
        private string _locationInfo;

        // Händelse som triggas när en egenskap ändras
        public event PropertyChangedEventHandler PropertyChanged;

        // Egenskap för platsinformation
        public string LocationInfo
        {
            get => _locationInfo;
            set
            {
                _locationInfo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LocationInfo)));
            }
        }

        // Hämtar den aktuella platsen
        public async Task GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;
                OnPropertyChanged(nameof(IsCheckingLocation));

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    LocationInfo = $"Lat: {location.Latitude}, Lon: {location.Longitude}, Alt: {location.Altitude}";
                }
            }
            catch (FeatureNotSupportedException)
            {
                LocationInfo = "Platsfunktioner stöds inte på denna enhet.";
            }
            catch (FeatureNotEnabledException)
            {
                LocationInfo = "Platsfunktionen är inte aktiverad.";
            }
            catch (PermissionException)
            {
                LocationInfo = "Behörigheter saknas för att hämta plats.";
            }
            catch (Exception ex)
            {
                LocationInfo = $"Fel: {ex.Message}";
            }
            finally
            {
                _isCheckingLocation = false;
                OnPropertyChanged(nameof(IsCheckingLocation));
            }
        }

        // Avbryter platsförfrågan
        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && !_cancelTokenSource.IsCancellationRequested)
                _cancelTokenSource.Cancel();
        }

        // Egenskap som indikerar om platsen kontrolleras
        public bool IsCheckingLocation => _isCheckingLocation;

        // Triggar PropertyChanged-händelsen
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Hämtar koordinaterna (latitud och longitud) asynkront
        public async Task<(double Latitude, double Longitude)> GetCoordinatesAsync()
        {
            try
            {
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");

                    return (location.Latitude, location.Longitude);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid hämtning av plats: {ex.Message}");
            }

            return (0, 0); // Returnera 0,0 om vi misslyckas
        }
    }
}
