using Fränder.Game;
using Fränder.Models;

namespace Fränder;

public partial class MainGamePage : TabbedPage
{
    private NeedsLogic needsLogic;
    private string frandName;
    //private Graveyard graveyard;
    private DateTime birthDate;

    private readonly WeatherService.WeatherService _weatherService;
    private readonly GeoLocation _geoLocation;
    private FoodShop _foodShop;
    public MainGamePage(string name)
    {
        InitializeComponent();
        frandName = name;
        needsLogic = new NeedsLogic(frandName);
        birthDate = DateTime.Now;
        //graveyard = new Graveyard();

        needsLogic.OnHungerChanged += UpdateHungerImage;
        needsLogic.OnCleanChanged += UpdateCleanImage;
        needsLogic.OnHealthChanged += UpdateHealthImage;
        needsLogic.OnMoneyChanged += UpdateMoneyDisplay;
        needsLogic.OnHealthChanged += CheckIfFrandDied;

        UpdateHungerImage();
        UpdateCleanImage();
        UpdateHealthImage();
        UpdateFrandName();
        UpdateMoneyDisplay();


        _foodShop = FoodShop.Instance(needsLogic);

        _weatherService = new WeatherService.WeatherService();
        _geoLocation = new GeoLocation();
    }

    private void UpdateFrandName()
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            FrandNameLabel.Text = $"{frandName}";
        });
    }

    private void CheckIfFrandDied()
    {
        if (needsLogic.IsDead)
        {
            GraveYard.MarkAsDead(frandName);
            DisplayAlert("Din Fränd har dött", $"{frandName} har gått bort...", "OK");
            Navigation.PopToRootAsync(); // Gå tillbaka till startmenyn
        }
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
                    UpdateMoneyDisplay();
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

    private void UpdateMoneyDisplay()
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            if (MoneyLabel != null)
            {
                MoneyLabel.Text = needsLogic.GetMoney().ToString();
            }
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
    private int sleepHours = 1;
    private void OnSleepSliderChanged(object sender, ValueChangedEventArgs e)
    {
        sleepHours = (int)e.NewValue;
        SleepHoursLabel.Text = $"Sovtid: {sleepHours} timmar";
    }

    private void OnSleepButtonClicked(object sender, EventArgs e)
    {
        needsLogic.StartSleeping(sleepHours);
    }

    private void OnShowGraveyardClicked(object sender, EventArgs e)
    {
        //Borttagen pga crashar när försöker spara till fil
    }

    private void OnServitorClicked(object sender, EventArgs e)
    {
        needsLogic.Servitor();
    }

    private void OnInfluencerClicked(object sender, EventArgs e)
    {
        needsLogic.Influenser();
    }

    private void OnWorkClicked(object sender, EventArgs e)
    {
        needsLogic.Worker();
    }

    private void OnDeveloperClicked(object sender, EventArgs e)
    {
        needsLogic.Programer();
    }

    private void OnDuschButtonClicked(object sender, EventArgs e)
    {
        needsLogic.Duscha();
    }

    private void OnBadaButtonClicked(object sender, EventArgs e)
    {
        needsLogic.Bada();
    }
}