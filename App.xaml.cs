namespace PopupPage
{
    public partial class App : Application
    {
        public App() => InitializeComponent();

        protected override Window CreateWindow(IActivationState activationState) => new(new AppShell()) { Height = 600, Width = 400 };
    }
}