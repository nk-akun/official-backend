using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace KeJian.Core.Library.Extensions
{
    public static class StringExtensions
    {
        public static string GetMd5(this string value)
        {
            Console.WriteLine(value);
            using var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            var sBuilder = new StringBuilder();
            foreach (var b in data) sBuilder.Append(b.ToString("x2"));

            var hash = sBuilder.ToString(); // 32位 小写
            return hash;
        }
    }
}