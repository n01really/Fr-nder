using Fränder.Game;

namespace Fränder;

public partial class MainGamePage : TabbedPage
{
    private NeedsLogic needsLogic = new NeedsLogic();

    private readonly WeatherService.WeatherService _weatherService;
    private readonly GeoLocation _geoLocation;
    private FoodShop _foodShop;
    public MainGamePage()
    {
        InitializeComponent();
        needsLogic.OnHungerChanged += UpdateHungerImage;
        needsLogic.OnCleanChanged += UpdateCleanImage;
        needsLogic.OnHealthChanged += UpdateHealthImage;


        UpdateHungerImage();
        UpdateCleanImage();
        UpdateHealthImage();



        _foodShop = FoodShop.Instance(needsLogic);

        _weatherService = new WeatherService.WeatherService();
        _geoLocation = new GeoLocation();
    }


    private void InitializeFoodButtons()
    {
        FoodButtonsLayout.Children.Clear();

        var foodItems = _foodShop.GetFoodItems();

        foreach (var foodItem in foodItems)
        {
            var button = new Button
            {
                Text = foodItem.Name + " - " + foodItem.Price,
                FontSize = 18
            };

            button.Clicked += (sender, args) =>
            {
                bool success = _foodShop.BuyFood(foodItem.Name);
                if (success)
                {
                    DisplayAlert("köp lyckades!", $"Du köpte {foodItem.Name}", "OK");
                }
                else
                {
                    DisplayAlert("Köp misslyckades ", "inte nog med pengar", "OK");
                }
            };

            FoodButtonsLayout.Children.Add(button);
        }
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
        InitializeFoodButtons();
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