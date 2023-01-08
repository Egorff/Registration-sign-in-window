using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace SHA512
{
    public static class HASH
    {
        public static string HashPass(byte[] bytes, byte[] salt, Encoding enc)
        {
            long bsLength = bytes.Length + salt.Length;

            long bLength = bytes.Length;

            long sLength = salt.Length;

            int jTemp = (int)bLength;

            byte[] bytes_salt = new byte[bsLength];

            for (int i = 0; i < bLength; i++)
            {
                bytes_salt[i] = bytes[i];
            }

            for (int i = 0; i < sLength; i++)
            {
                for (int j = jTemp; j < bsLength;)
                {
                    bytes_salt[j] = salt[i];

                    j += 1;

                    jTemp = j;

                    break;
                }
            }

            SHA512Managed shaM = new SHA512Managed();

            var hash = shaM.ComputeHash(bytes_salt);

            return enc.GetString(hash);
        }
    }
}