using Fränder.Game;
using Fränder.Models;

namespace Fränder;

public partial class MainGamePage : TabbedPage
{
    private NeedsLogic needsLogic;
    private string frandName;
    private DateTime birthDate;

    private readonly WeatherService.WeatherService _weatherService;
    private readonly GeoLocation _geoLocation;
    private FoodShop _foodShop;

    // Konstruktor för MainGamePage, initialiserar nödvändiga komponenter och logik
    public MainGamePage(string name)
    {
        InitializeComponent();
        frandName = name;
        needsLogic = new NeedsLogic(frandName);
        birthDate = DateTime.Now;

        needsLogic.OnHungerChanged += UpdateHungerImage;
        needsLogic.OnCleanChanged += UpdateCleanImage;
        needsLogic.OnHealthChanged += UpdateHealthImage;
        needsLogic.OnMoneyChanged += UpdateMoneyDisplay;

        UpdateHungerImage();
        UpdateCleanImage();
        UpdateHealthImage();
        UpdateFrandName();
        UpdateMoneyDisplay();

        _foodShop = FoodShop.Instance(needsLogic);

        _weatherService = new WeatherService.WeatherService();
        _geoLocation = new GeoLocation();
    }

    // Uppdaterar Frändens namn på huvudtråden
    private void UpdateFrandName()
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            FrandNameLabel.Text = $"{frandName}";
        });
    }

    //private async void CheckIfFrandDied()
    //{
    //    try
    //    {
    //        if (needsLogic.IsDead)
    //        {
    //            if (!string.IsNullOrEmpty(frandName))
    //            {
    //                GraveYard.MarkAsDead(frandName);
    //                await DisplayAlert("Din Fränd har dött", $"{frandName} har gått bort...", "OK");
    //                await Navigation.PopToRootAsync(); // Gå tillbaka till startmenyn
    //            }
    //            else
    //            {
    //                await DisplayAlert("Error", "Frand name is null or empty.", "OK");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
    //    }
    //}

    // Initialiserar matknapparna i layouten
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

    // Uppdaterar hungerbilden på huvudtråden
    private void UpdateHungerImage()
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            hungerImage.Source = needsLogic.GetImage();
        });
    }

    // Uppdaterar renhetsbilden på huvudtråden
    private void UpdateCleanImage()
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            cleanImage.Source = needsLogic.GetCleanImage();
        });
    }

    // Uppdaterar hälsobilden på huvudtråden
    private void UpdateHealthImage()
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            healthImage.Source = needsLogic.GetHealthImage();
        });
    }

    // Uppdaterar pengavisningen på huvudtråden
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

    // Körs när sidan visas, hämtar väder och initialiserar matknappar
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await GetWeatherAutomatically();
        InitializeFoodButtons();
    }

    // Hämtar väderinformation automatiskt baserat på koordinater
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

    // Hanterar förändring av sovtid via en slider
    private void OnSleepSliderChanged(object sender, ValueChangedEventArgs e)
    {
        sleepHours = (int)e.NewValue;
        SleepHoursLabel.Text = $"Sovtid: {sleepHours} timmar";
    }

    // Hanterar klickhändelse för sovknappen
    private void OnSleepButtonClicked(object sender, EventArgs e)
    {
        needsLogic.StartSleeping(sleepHours);
    }

    // Hanterar klickhändelse för att visa kyrkogården
    private void OnShowGraveyardClicked(object sender, EventArgs e)
    {
        //Borttagen pga crashar när försöker spara till fil
    }

    // Hanterar klickhändelse för servitorknappen
    private void OnServitorClicked(object sender, EventArgs e)
    {
        needsLogic.Servitor();
    }

    // Hanterar klickhändelse för influencerkknappen
    private void OnInfluencerClicked(object sender, EventArgs e)
    {
        needsLogic.Influenser();
    }

    // Hanterar klickhändelse för arbetsknappen
    private void OnWorkClicked(object sender, EventArgs e)
    {
        needsLogic.Worker();
    }

    // Hanterar klickhändelse för utvecklarknappen
    private void OnDeveloperClicked(object sender, EventArgs e)
    {
        needsLogic.Programer();
    }

    // Hanterar klickhändelse för duschknappen
    private void OnDuschButtonClicked(object sender, EventArgs e)
    {
        needsLogic.Duscha();
    }

    // Hanterar klickhändelse för badknappen
    private void OnBadaButtonClicked(object sender, EventArgs e)
    {
        needsLogic.Bada();
    }
}
