using System.Runtime.InteropServices;
using System.Security;

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
    }
}