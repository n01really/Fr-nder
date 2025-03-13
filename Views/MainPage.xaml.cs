using Fränder.Game;
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
            await Navigation.PushAsync(new MainGamePage());
        }

        private void OnClickedQuitGame(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }
    }

}
