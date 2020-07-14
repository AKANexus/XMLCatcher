using System;
using System.Text;

namespace XMLCatcherMkII_Installer
{
    class Encryption
    {
        public string EncodeText(string texto)
        {
            var bytes = Encoding.UTF8.GetBytes(texto);
            return Convert.ToBase64String(bytes);
        }

        public string DecodeText(string encodedText)
        {
            var bytes = Convert.FromBase64String(encodedText);
            return Encoding.UTF8.GetString(bytes);
        }

    }

}
