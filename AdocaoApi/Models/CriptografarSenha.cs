using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdocaoApi.Models
{
    public static class CriptografarSenha
    {
        public static string HashPassword(string password)
        {
            var provider = new SHA1CryptoServiceProvider();
            var encoding = new UnicodeEncoding();
            var updatedPassword = provider.ComputeHash(encoding.GetBytes(password));
            return Convert.ToBase64String(updatedPassword);
        }
    }
}
