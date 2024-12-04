using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class PasswordModel
    {
        //Implement the Password Model here.

        public int ID { get; set; }
        public int UserID { get; set; }
        public string PlatformName { get; set; }
        private byte[] EncryptedPassword { get; set; }
        private Tuple<byte[], byte[]> EncryptionKeys { get; set; }

        // constructor

        public PasswordModel(int id, int userId, string platformName, string password, Tuple<byte[], byte[]> encryptionKeys)
        {
            ID = id;
            UserID = userId;
            PlatformName = platformName;
            EncryptionKeys = encryptionKeys;
            SetPassword(password);
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.");

            EncryptedPassword = PasswordCrypto.Encrypt(password, EncryptionKeys);
        }

        public string GetPassword()
        {
            return PasswordCrypto.Decrypt(EncryptedPassword, EncryptionKeys);
        }

    }
}

