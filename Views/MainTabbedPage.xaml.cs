using Microsoft.Maui.Controls;

namespace Fränder
{
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage()
        {
            InitializeComponent();
        }

        private void FeedFrand(object sender, EventArgs e)
        {
            DisplayAlert("Matning", "Du har matat din Fränd!", "OK");
        }

        private void SleepFrand(object sender, EventArgs e)
        {
            DisplayAlert("Sömn", "Din Fränd sover nu.", "OK");
        }
    }
}