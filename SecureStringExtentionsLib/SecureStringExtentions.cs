using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SecureStringExtentionsLib
{
    public static class SecureStringExtentions
    {
        public unsafe static bool Compare(this SecureString str1, SecureString str2)
        {
            if (str1.Length != str2.Length)
            {
                return false;
            }

            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;

            try
            {
                bstr1 = Marshal.SecureStringToBSTR(str1);

                bstr2 = Marshal.SecureStringToBSTR(str2);

                char* ptr1 = (char*)bstr1.ToPointer();

                char* ptr2 = (char*)bstr2.ToPointer();

                for (;*ptr1 != 0 && *ptr2 != 0; ++ptr1, ++ptr2)
                {
                    if (*ptr1 != *ptr2)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Marshal.FreeBSTR(bstr1);

                Marshal.FreeBSTR(bstr2);
            }

            return true;
        }

        public unsafe static byte[] GetBytesAccordingToEncoding(this SecureString str, Encoding e)
        {
            IntPtr bstr1 = IntPtr.Zero;

            byte[] bytes;

            if (str.Length == 0)
            {
                return null; 
            }

            bytes = null;

            try
            { 
                bstr1 = Marshal.SecureStringToBSTR(str);

                char[] chars = new char[str.Length];

                char* ptr1 = (char*)bstr1.ToPointer();

                for (int i = 0; *ptr1 != 0; ++ptr1, i++)
                {
                    chars[i] = *ptr1;
                }

                bytes = e.GetBytes(chars);

                chars = null;
            }
            catch (Exception)
            {

            }
            finally
            {
                Marshal.FreeBSTR(bstr1);
            }

            return bytes;
        }
    }
}