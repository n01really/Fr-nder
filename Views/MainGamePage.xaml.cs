using Fränder.Game;

namespace Fränder;

public partial class MainGamePage : TabbedPage
{
    private NeedsLogic needsLogic = new NeedsLogic();

    private readonly WeatherService.WeatherService _weatherService;
    private readonly GeoLocation _geoLocation;
    public MainGamePage()
    {
        InitializeComponent();
        needsLogic.OnHungerChanged += UpdateHungerImage;
        needsLogic.OnCleanChanged += UpdateCleanImage;
        needsLogic.OnHealthChanged += UpdateHealthImage;
        UpdateHungerImage();
        UpdateCleanImage();
        UpdateHealthImage();
        _weatherService = new WeatherService.WeatherService();
        _geoLocation = new GeoLocation();
    }
    private void UpdateHungerImage()
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            hungerImage.Source = needsLogic.GetImage();
        });
    }

    private void UpdateCleanImage()
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            cleanImage.Source = needsLogic.GetCleanImage();
        });
    }

    private void UpdateHealthImage()
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            healthImage.Source = needsLogic.GetHealthImage();
        });
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetWeatherAutomatically();
    }
    private async Task GetWeatherAutomatically()
    {
        try
        {
            (double latitude, double longitude) = await _geoLocation.GetCoordinatesAsync();

            if (latitude == 0 && longitude == 0)
            {
                latitude = 59.3293;
                longitude = 18.0686;
                return;
            }

            var weather = await _weatherService.GetWeatherByCoordinatesAsync(latitude, longitude);

            if (weather != null)
            {
               TemperatureLabel.Text = $"{weather.Temp}°C"; 
            }
            else
            {
                TemperatureLabel.Text = "API fel";
            }
        }
        catch (Exception)
        {
            TemperatureLabel.Text = "Fel";
        }
    }
}