using CommunityToolkit.Maui.Extensions;
using System.Diagnostics;

namespace PopupPage;

public partial class SplashPage : ContentPage
{
    public SplashPage()
    {
        InitializeComponent();
        this.Loaded += SplashPage_Loaded;
    }

    private int splashNesting = 0;
    private async void SplashPage_Loaded(object sender, EventArgs e)
    {
        int myNesting = splashNesting++;
        Debug.WriteLine($">>> SplashPage.SplashPage_Loaded() - Start level {myNesting}");
        base.OnAppearing();
        await SimulatedInitialization();
        splashNesting--;
        Debug.WriteLine($">>> SplashPage.SplashPage_Loaded() - End level {myNesting}");
        await DisplayAlert("Simulated Initialization Complete", "Tap OK to go to the home page", "OK");
        await Shell.Current.GoToAsync("//MainPage"); // Lets the user know this is finished
    }

    private async Task SimulatedInitialization()
    {
        Debug.WriteLine(">>> SplashPage.SimulatedInitialization() - Start");

        //await Task.Delay(100); // Without this delay the popup will not show

        await this.ShowPopupAsync(new Label
        {
            Text = "Tap to dismiss this popup",
            BackgroundColor = Colors.Red
        });

        Debug.WriteLine(">>> SplashPage.SimulatedInitialization() - Complete");
    }
}