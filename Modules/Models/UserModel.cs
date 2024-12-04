using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class User
    {

        //Implement the User Model here. //

        public int ID { get; set; } // User's ID
        public string FirstName { get; set; } // First Name
        public string LastName { get; set; } // Last Name
        public string UserName { get; set; } // User's Username
        public byte[] PasswordHash { get; private set; } // Hiding User's Password
        public byte[] Key { get; private set; } // Encryption Key
        public byte[] IV { get; private set; } // Vector for Encryption


        // Constructor //

        public User(string firstName, string lastName, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;

            var encryptionKeys = PasswordCrypto.GenKey();
            Key = encryptionKeys.Item1;
            IV = encryptionKeys.Item2;

            SetPassword(password);
        }

        public void SetPassword(string password)
        {
            PasswordHash = PasswordCrypto.GetHash(password) ?? throw new ArgumentNullException("Password cannot be null");
        }

        public bool VerifyPassword(string password)
        {
            var hashToCheck = PasswordCrypto.GetHash(password);
            return hashToCheck != null && PasswordCrypto.CompareBytes(hashToCheck, PasswordHash);
        }

    }
}
