using Fränder.Game;

namespace Fränder
{
    public partial class MainPage : ContentPage
    {
        private NeedsLogic needsLogic = new NeedsLogic();

        public MainPage()
        {
            InitializeComponent();
            needsLogic.OnHungerChanged += UpdateHungerImage;
            UpdateHungerImage();
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





    }

}
