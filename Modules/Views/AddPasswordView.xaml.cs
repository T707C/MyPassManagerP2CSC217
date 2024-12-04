namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class AddPasswordView : ContentPage
{
    Color baseColorHighlight;
    private bool _generatedPassword = false; // was once bool generatedPassword(incase it breaks)

    public AddPasswordView()
    {
        InitializeComponent();
        //Stores the original color of the text boxes. Used to revert a text box back
        //to its original color if it was "highlighted" during input validation.
        baseColorHighlight = (Color)Application.Current.Resources["ErrorEntryHighlightBG"];

        //Determines if a password was generated at least once.
        //generatedPassword = false;
    }

    private void ButtonCancel(object sender, EventArgs e)
    {
        //Called when the Cancel button is clicked.
        Navigation.PopAsync();
    }

    private void ButtonSubmitExisting(object sender, EventArgs e)
    {
        //Called when the Submit button is clicked for a password manually
        //entered.  (i.e., existing password).

        if (string.IsNullOrWhiteSpace(txtNewPlatform.Text) ||
            string.IsNullOrWhiteSpace(txtNewUserName.Text) ||
            string.IsNullOrWhiteSpace(txtNewPassword.Text))
        {
            lblErrorExistingPassword.Text = "All fields must be fileld out!";
            lblErrorExistingPassword.IsVisible = true;
            return;
        }

        // Add the password
        App.PasswordController.AddPassword(txtNewPlatform.Text, txtNewUserName.Text, txtNewPassword.Text);
        Navigation.PopAsync();
    }

    private void ButtonSubmitGenerated(object sender, EventArgs e)
    {
        //Called when the submit button for a Generated password is clicked.
        if (string.IsNullOrWhiteSpace(txtNewPlatform.Text) ||
            string.IsNullOrWhiteSpace(txtNewUserName.Text) ||
            !_generatedPassword)
        {
            lblErrorGeneratedPassword.Text = "Platform name, usernmae, and a good secure password are required.";
            lblErrorGeneratedPassword.IsVisible = true;
            return;
        }
        App.PasswordController.AddPassword(txtNewPlatform.Text, txtNewUserName.Text, lblGenPassword.Text);
        Navigation.PopAsync();
    }

    private void ButtonGeneratePassword(object sender, EventArgs e)
    {
        //Called when the Generate Password button is clicked.
        string generatedPassword = PasswordGeneration.BuildPassword(
            upper_letter: true,
            digit: true,
            req_symbols: "!@#$%",
            min_length: 12);

        // display the password

        lblGenPassword.Text = generatedPassword;
        _generatedPassword = true;
    }
}