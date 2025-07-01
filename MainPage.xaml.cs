using System.Diagnostics;

namespace PopupPage
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }
        private int loadedNesting = 0;
        private bool splashed = false; // Has automatic navigation to the splash page been done yet?
        private async void MainPage_Loaded(object sender, EventArgs e)
        {
            int myNesting = loadedNesting++;
            Debug.WriteLine($">>> MainPage_Loaded - Start level {myNesting}");
            if (!splashed)
            {
                splashed = true; // Disable automatic switch to the splash page, we only do it once
                await Shell.Current.GoToAsync("//SplashPage"); // Navigate to the splash page 
            }
            Debug.WriteLine($">>> MainPage_Loaded - End level {myNesting}");
            loadedNesting--;
        }
    }
}
