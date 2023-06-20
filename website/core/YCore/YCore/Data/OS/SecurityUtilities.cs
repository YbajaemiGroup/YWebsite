using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCore.Data.OS
{
    internal static class SecurityUtilities
    {
#warning тут мб надо будет еще что-нибудь дописать
        /// <summary>
        /// True if ok
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static bool ValidateImageName(string imageName)
        {
            return !(imageName.Contains('\\') ||
                imageName.Contains("..") ||
                imageName.Contains('/') ||
                imageName.Contains('\'') ||
                imageName.Contains('~') ||
                imageName.Contains('!') ||
                imageName.Contains('@') ||
                imageName.Contains('%') ||
                imageName.Contains('|') ||
                imageName.Contains('&') ||
                imageName.Contains(';'));
        }
    }
}
