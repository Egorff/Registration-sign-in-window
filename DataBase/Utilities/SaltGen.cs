using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Utilities
{
    public static class SaltGen
    {
        public static string GenerateSalt(int length)
        {
            StringBuilder sb = new StringBuilder();

            Random r = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append((char)r.Next(33, 127));
            }

            return sb.ToString();
        }
    }
}
