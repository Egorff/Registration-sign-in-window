using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace SHA512
{
    public static class HASH
    {
        public static string HashPass(byte[] bytes, byte[] salt, Encoding enc)
        {
            byte[] bytes_salt = new byte[bytes.Length + salt.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes_salt[i] = bytes[i];
            }

            for (int i = 0; i < salt.Length; i++)
            {
                bytes_salt[i] = salt[i];
            }

            SHA512Managed shaM = new SHA512Managed();

            var hash = shaM.ComputeHash(bytes_salt);

            return enc.GetString(hash);
        }
    }
}