namespace Fränder
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainTabbedPage();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}