using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Controls.Shapes;
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
        private readonly bool splashed = false; // Has automatic navigation to the splash page been done yet?
        private async void MainPage_Loaded(object sender, EventArgs e)
        {
            int myNesting = loadedNesting++;
            Debug.WriteLine($">>> MainPage_Loaded - Start level {myNesting}");
            if (splashed)
            {
                IsVisible = true;
            }
            else
            {
                IsVisible = false;
                //await Task.Delay(50); // Let pending navigations settle down, without this the following navigation will throw an exception
                await Shell.Current.GoToAsync("//SplashPage"); // Navigate to the splash page 
            }
            loadedNesting--;
        }
        #region Utility Functions
        internal static PopupOptions GetNullPopupOptions(bool CanBeDismissedByTappingOutsideOfPopup = true) => new()
        {
            CanBeDismissedByTappingOutsideOfPopup = CanBeDismissedByTappingOutsideOfPopup,
            Shape = null,
            Shadow = null
        };
        #endregion
        private int appearingNesting = 0;
        protected override void OnAppearing()
        {
            int myNesting = appearingNesting++;
            Debug.WriteLine($">>> MainPage.OnAppearing() - Start level {myNesting}");
            base.OnAppearing();
            Debug.WriteLine($">>> MainPage.OnAppearing() - End level {myNesting}");
            appearingNesting--;
        }
        #region Allow light/dark mode switching
        public bool Dark
        {
            set
            {
                if (value != Dark)
                    Application.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
            }
            get => Application.Current.UserAppTheme == AppTheme.Dark || Application.Current.RequestedTheme == AppTheme.Dark;
        }

        #endregion        
        #region DivisiBill Pages
        private async void OnQuestionClicked(object sender, EventArgs e)
        {
            QuestionPage questionPage = new("Question Title", "Is this a very important message?", true);
            IPopupResult<QuestionResponse> popupResult = await Shell.Current.ShowPopupAsync<QuestionResponse>(questionPage, GetNullPopupOptions(false));
            QuestionResponse d = popupResult.Result;
            bool ask = d.Ask;
            bool yes = d.Yes;
            await DisplayAlert("Response", $"Ask Again={ask}, Yes={yes}!", "OK");
        }
        private async void OnAppSnackBarClicked(object sender, EventArgs e)
        {
            var snackBar = new AppSnackBarPage("This message will close in 5 seconds");
            await Shell.Current.ShowPopupAsync(snackBar, GetNullPopupOptions());
        }
        private async void OnPaymentsClicked(object sender, EventArgs e)
        {
            PaymentsViewModel paymentsViewModel = new(80, 100, "Fred", 5, 2);
            await Shell.Current.ShowPopupAsync(new PaymentsPage(paymentsViewModel), GetNullPopupOptions());
        }
        #endregion
        #region Sample Pages
        /// <summary>
        /// A Popup Generated solely in C# at the call site, no other class or XAML involved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPopupClicked(object sender, EventArgs e)
        {
            await this.ShowPopupAsync(new Label
            {
                Text = "This must be dismissed manually",
                BackgroundColor = Colors.Red,
                VerticalOptions = LayoutOptions.End
            }, new PopupOptions
            {
                CanBeDismissedByTappingOutsideOfPopup = true,

                Shape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(20, 20, 20, 20),
                    StrokeThickness = 2,
                    Stroke = Colors.LightGray
                }
            });
        }
        /// <summary>
        /// A popup that returns a result, copied from the documentation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnReturnResultClicked(object sender, EventArgs e)
        {
            var popup = new ReturnResultPopup();

            // The type parameter must match the type returned from the popup.
            var popupResult = await this.ShowPopupAsync<bool>(popup, GetNullPopupOptions());
            if (popupResult.WasDismissedByTappingOutsideOfPopup)
                await DisplayAlert("Dismissed", "You dismissed the popup by tapping outside of it.", "OK");
            else if (popupResult.Result)
                await DisplayAlert("Result", "The popup returned a positive result!", "OK");
            else
                await DisplayAlert("Result", "The popup returned a negative result.", "OK");
        }
        private async void OnSimplePopupClicked(object sender, EventArgs e)
        {
            await this.ShowPopupAsync(new Label
            {
                Text = "This is a simple popup that must be dismissed manually",
                BackgroundColor = Colors.Red
            });
        }
        private async void OnStaticSnackBarClicked(object sender, EventArgs e) => await ShowSimpleSnackBarAsync("This is a static SnackBar, dismiss it manually");

        /// <summary>
        /// Whatever popup logic we're trying
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnTrialClicked(object sender, EventArgs e)
        {
            Shell.Current.Navigating += Cancel_Navigation;
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await this.ShowPopupAsync(new Label
                {
                    Text = "This simple popup must be dismissed manually",
                    BackgroundColor = Colors.Red,
                    VerticalOptions = LayoutOptions.End
                });
            });
        }
        private async void Cancel_Navigation(object sender, ShellNavigatingEventArgs e)
        {
            if (false && e.CanCancel)
                e.Cancel();
            await Task.Delay(5_000);
        }

        /// <summary>
        /// Show an in-line SnackBar type implementation using a static function.
        /// Does not handle automatically closing.
        /// </summary>
        /// <param name="message"></param>
        internal static Task ShowSimpleSnackBarAsync(string message)
        {
            var label = new Label
            {
                Text = message,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            var popup = new CommunityToolkit.Maui.Views.Popup
            {
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.Red,
                Margin = new Thickness(10, 5, 10, 5),
                Padding = 0,
                Content = label
            };
            return Shell.Current.ShowPopupAsync(popup, new PopupOptions
            {
                CanBeDismissedByTappingOutsideOfPopup = true,

                Shape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(10, 10, 10, 10),
                    StrokeThickness = 0
                }
            });
        }
        #endregion
    }
}
