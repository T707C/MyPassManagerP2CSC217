using CSC317PassManagerP2Starter.Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public enum AuthenticationError { NONE, INVALIDUSERNAME, INVALIDPASSWORD }
    public class LoginController
    {

        /*
         * This class is incomplete.  Fill in the method definitions below.
         */
        private User _user = new User("John", "Doe", "test", "ab1234");
        private bool _loggedIn = false;

        // Constructor for test user

        public LoginController()
        {
            _user = new User("John", "Doe", "test", "ab1234");
        }
        public User? CurrentUser
        {
            get
            {
                //Returns a copy of the user data.  Currently returning null.
                return _loggedIn ? new User(_user.FirstName, _user.LastName, _user.UserName, "******") : null;
            }
        }

        public AuthenticationError Authenticate(string username, string password)
        {
            //determines whether the inputted username/password matches the stored

            if (username != _user.UserName)
                return AuthenticationError.INVALIDUSERNAME;

            //username
            ///password.  currently returns a NONE error status.

            if (!_user.VerifyPassword(password))
                return AuthenticationError.INVALIDPASSWORD;


            // when both match

            _loggedIn = true;
            return AuthenticationError.NONE;
        }
    }

}

