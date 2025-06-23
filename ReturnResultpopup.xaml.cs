using CommunityToolkit.Maui.Views;

#nullable enable
namespace PopupPage;

public partial class ReturnResultPopup : Popup<bool>
{
    public ReturnResultPopup() => InitializeComponent();

    private async void OnYesButtonClicked(object? sender, EventArgs e) => await CloseAsync(true);

    private async void OnNoButtonClicked(object? sender, EventArgs e) => await CloseAsync(false);
}