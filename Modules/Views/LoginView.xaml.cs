namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }

    // The Login process 
    private void ProcessLogin(object sender, EventArgs e)
    {
        //Complete Process Login Functionality.  Called by Submit Button

        string username = txtUserName.Text?.Trim() ?? string.Empty;
        string password = txtPassword.Text ?? string.Empty;

        // Authenticator

        var authResult = App.LoginController.Authenticate(username, password);

        // Error

        if (authResult == Controllers.AuthenticationError.INVALIDUSERNAME)
        {
            ShowErrorMessage("Invalid Username. Please try again.");
            return;
        }

        else if (authResult == Controllers.AuthenticationError.INVALIDPASSWORD)
        {
            ShowErrorMessage("Incorrect Password. Please try again.");
            return;
        }

        // Goes to Password List page

        Navigation.PushAsync(new PasswordListView());
    }

    private void ShowErrorMessage(string message)
    {
        //Complete ShowError Message functionality.  Supports ProcessLogin.

        lblError.Text = message;
        lblError.IsVisible = true;
    }
}