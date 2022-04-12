using System.Security.Cryptography;
using System.Text;

namespace ISOB_2_Kerberos
{
    class Client
    {
        public string UserName { get; private set; }
        private string Password { get; set; }

        public string PasswordHash
        {
            get => GetPasswordHash();
        }

        public Client(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string GetPasswordHash()
        {
            var tmpSource = Encoding.ASCII.GetBytes(Password);
            var tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            var value = new StringBuilder(tmpHash.Length);
            foreach (byte b in tmpHash)
            {
                value.Append(b.ToString("X2"));
            }

            return value.ToString();
        }
    }
}