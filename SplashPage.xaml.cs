using CommunityToolkit.Maui.Extensions;
using System.Diagnostics;

namespace PopupPage;

public partial class SplashPage : ContentPage
{
    public SplashPage() => InitializeComponent();

    private bool initializationCalled = false;
    private int nesting = 0;
    protected override async void OnAppearing()
    {
        int myNesting = nesting++;
        Debug.WriteLine($">>> SplashPage: OnAppearing() - Start level {myNesting}");
        base.OnAppearing();

        if (!initializationCalled)
        {
            initializationCalled = true;
            await SimulatedInitialization();
        }
        nesting--;
        Debug.WriteLine($">>> SplashPage: OnAppearing() - End level {nesting}");
        await Shell.Current.GoToAsync("//MainPage"); // Lets the user know this is finished
    }
    private async Task SimulatedInitialization()
    {
        Debug.WriteLine(">>> SplashPage: SimulatedInitialization() - Start");

        await Task.Delay(50); // Without this delay the popup will not show

        await this.ShowPopupAsync(new Label
        {
            Text = "Tap to dismiss this popup",
            BackgroundColor = Colors.Red
        });

        Debug.WriteLine(">>> SplashPage: SimulatedInitialization() - Complete");
    }
}