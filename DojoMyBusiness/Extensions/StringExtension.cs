using System;
using System.Security.Cryptography;
using System.Text;

namespace DojoMyBusiness.Extensions
{
    public static class StringExtension
    {
        public static string CalculateHash(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            using (var algorithm = MD5.Create()) 
            {
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashedBytes).Replace("-","").ToUpper();
            }
        }
    }
}
