using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC317PassManagerP2Starter.Modules.Controllers;
using CSC317PassManagerP2Starter.Modules.Models;
using CSC317PassManagerP2Starter.Modules.Views;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public class PasswordController
    {
        //Stores a list of sample passwords for the test user.
        private List<PasswordModel> _passwords = new List<PasswordModel>(); // Changed this from public to priavte //
        private int counter = 1;



        /*
         * The following functions need to be completed.
         */
        //Used to copy the passwords over to the Row Binders.

        public void PopulatePasswordView(ObservableCollection<PasswordRow> source, string search_criteria = "")
        {
            //Complete definition of PopulatePasswordView here.

            source.Clear();
            foreach (var password in _passwords)
            {
                if (string.IsNullOrWhiteSpace(search_criteria) || password.PlatformName.Contains(search_criteria, StringComparison.OrdinalIgnoreCase))
                {
                    source.Add(new PasswordRow(password));
                }
            }
        }

        //CRUD operations for the password list.
        public void AddPassword(string platform, string username, string password)
        {
            //Complete definition of AddPassword here.
            var user = App.LoginController.CurrentUser;
            if (user != null)
            {
                _passwords.Add(new PasswordModel(counter++, user.ID, platform, password, Tuple.Create(user.Key, user.IV)));
            }

        }

        public PasswordModel? GetPassword(int ID)
        {
            //Complete definition of GetPassword here.

            return _passwords.Find(p => p.ID == ID);

            return null;
        }

        public bool UpdatePassword(PasswordModel changes)
        {
            //Complete definition of Update Password here.

            var existingPassword = GetPassword(changes.ID);

            if (existingPassword != null)
            {
                existingPassword.SetPassword(changes.GetPassword());
                existingPassword.PlatformName = changes.PlatformName;
                return true;
            }

            return false;
        }

        public bool RemovePassword(int ID)
        {
            //Complete definition of Remove Password here.

            var password = GetPassword(ID);
            if (password != null)
            {
                _passwords.Remove(password);
                return true;
            }

            return false;
        }

        public void GenTestPasswords()
        {
            //Generate a set of random passwords for the test user.
            //Called in Password List Page.

            var encryptionKeys = App.LoginController.CurrentUser?.Key;
            var iv = App.LoginController.CurrentUser?.IV;

            if (encryptionKeys != null && iv != null)
            {
                _passwords.Add(new PasswordModel(counter++, 1, "Google", "luvgoogle23!", Tuple.Create(encryptionKeys, iv)));
                _passwords.Add(new PasswordModel(counter++, 1, "Facebook", "NeonCatLover17", Tuple.Create(encryptionKeys, iv)));
                _passwords.Add(new PasswordModel(counter++, 1, "Twitter", "ThereIsNoX", Tuple.Create(encryptionKeys, iv)));
            }
        }
    }
}
