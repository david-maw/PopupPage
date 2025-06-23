namespace PopupPage;

public partial class PaymentsPage : CommunityToolkit.Maui.Views.Popup
{
    public PaymentsPage(PaymentsViewModel paymentsViewModel)
    {
        BindingContext = paymentsViewModel;
        InitializeComponent();
    }
}