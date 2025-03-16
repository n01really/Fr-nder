using Fränder.Game;
using Fränder.Models;
using System.Threading.Tasks;


namespace Fränder
{
    public partial class MainPage : ContentPage
    {
        
        

        public MainPage()
        {
            InitializeComponent();

        }

        private async void OnNavigateClicked(object sender, EventArgs e)
        {
            string frandName = NameEntry.Text?.Trim();

            if (string.IsNullOrEmpty(frandName))
            {
                await DisplayAlert("Fel", "Vänligen ange ett namn för din Fränd!", "OK");
                return;
            }


            // Skapa en ny Fränd och spara i GraveYard
            GraveYard.AddFrand(frandName);

            await Navigation.PushAsync(new MainGamePage(frandName));
        }

        private void OnClickedQuitGame(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }

}
