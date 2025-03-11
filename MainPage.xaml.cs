﻿using Fränder.Game;

namespace Fränder
{
    public partial class MainPage : ContentPage
    {
        private NeedsLogic needsLogic = new NeedsLogic();
        

        public MainPage()
        {
            InitializeComponent();
            needsLogic.OnHungerChanged += UpdateHungerImage;
            needsLogic.OnCleanChanged += UpdateCleanImage;
            needsLogic.OnHealthChanged += UpdateHealthImage;
            UpdateHungerImage();
            UpdateCleanImage();
            UpdateHealthImage();
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





    }

}
