using System.Collections.ObjectModel;

namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class PasswordListView : ContentPage
{
    private ObservableCollection<PasswordRow> _rows = new ObservableCollection<PasswordRow>();

    public PasswordListView()
    {
        InitializeComponent();

        //once logged in, generate a set of test passwords for the user.
        App.PasswordController.GenTestPasswords();

        //Populates the list of shown passwords  This should also be called in the search
        //bar event method to implement the search filter.
        App.PasswordController.PopulatePasswordView(_rows);

        //Binds the Collection View to the password rows.
        collPasswords.ItemsSource = _rows;
    }

    private void TextSearchBar(object sender, TextChangedEventArgs e)
    {
        //Is binded to the Search Bar.  Calls PopulatePasswords from the Password Controller.
        //to update the list of shown passwords.

        App.PasswordController.PopulatePasswordView(_rows, e.NewTextValue);
    }

    private void CopyPassToClipboard(object sender, EventArgs e)
    {
        //Is called when Copy is clicked.  Looks up the password by its ID
        //and copies it to the clipboard.

        //Example of how to get the ID of the password selected.
        int ID = Convert.ToInt32((sender as Button)?.CommandParameter);
        var password = App.PasswordController.GetPassword(ID);

        if (password != null)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Clipboard.Default.SetTextAsync(password.GetPassword());
            });
        }
    }

    private void EditPassword(object sender, EventArgs e)
    {
        //Called when Edit/Save is clicked.

        int ID = Convert.ToInt32((sender as Button)?.CommandParameter);
        var row = _rows.FirstOrDefault(r => r.PasswordID == ID);

        if (row != null)
        {
            row.SavePassword();
            row.Editing = false;
        }
        else
        {
            row.Editing = true;
        }
    }

    private void DeletePassword(object sender, EventArgs e)
    {
        //Called when Delete is clicked.

        int ID = Convert.ToInt32((sender as Button)?.CommandParameter);
        App.PasswordController.RemovePassword(ID);
        App.PasswordController.PopulatePasswordView(_rows);
    }

    private void ButtonAddPassword(object sender, EventArgs e)
    {
        //Called when Add Password is clicked.  

        Navigation.PushAsync(new AddPasswordView());
    }
}