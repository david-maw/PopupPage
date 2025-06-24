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
        Debug.WriteLine($">>> SplashPage: SplashPage_Loaded() - Start level {myNesting}");
        base.OnAppearing();
        await SimulatedInitialization();
        splashNesting--;
        Debug.WriteLine($">>> SplashPage: SplashPage_Loaded() - End level {myNesting}");
        await Shell.Current.GoToAsync("//MainPage"); // Lets the user know this is finished
    }

    private int appearingNesting = 0;
    protected override void OnAppearing()
    {
        int myNesting = appearingNesting++;
        Debug.WriteLine($">>> SplashPage: OnAppearing() - Start level {myNesting}");
        base.OnAppearing();
        appearingNesting--;
        Debug.WriteLine($">>> SplashPage: OnAppearing() - End level {myNesting}");
    }
    private int navigatedToNesting = 0;
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        int myNesting = navigatedToNesting++;
        Debug.WriteLine($">>> SplashPage: OnNavigatedTo() - Start level {myNesting}");
        base.OnNavigatedTo(args);
        navigatedToNesting--;
        Debug.WriteLine($">>> SplashPage: OnNavigatedTo() - End level {myNesting}");
    }
    private async Task SimulatedInitialization()
    {
        Debug.WriteLine(">>> SplashPage: SimulatedInitialization() - Start");

        //await Task.Delay(100); // Without this delay the popup will not show

        await this.ShowPopupAsync(new Label
        {
            Text = "Tap to dismiss this popup",
            BackgroundColor = Colors.Red
        });

        Debug.WriteLine(">>> SplashPage: SimulatedInitialization() - Complete");
    }
}