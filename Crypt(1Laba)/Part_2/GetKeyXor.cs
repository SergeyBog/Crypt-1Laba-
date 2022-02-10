using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypt_1Laba_.Part_2
{
    public class GetKeyXor
    {
        public void GetByteArr() {
            var base64 = File.ReadAllText(@"C:\Users\Sergey\source\repos\Crypt(1Laba)\Crypt(1Laba)\Part_2\Task2.txt");
            byte[]text = Convert.FromBase64String(base64);
            var encodeText = Encoding.ASCII.GetString(text);
            FindKeyLength(encodeText);
        }
        public static void FindKeyLength(string encodedText)
        {
            for (int i = 0; i < encodedText.Length; i++)
            {
                var n = 0;
                var offsetText = encodedText.Substring(encodedText.Length - i, i) + encodedText.Substring(0, encodedText.Length - i);

                for (int j = 0; j < encodedText.Length; j++)
                {
                    if (offsetText[j] == encodedText[j])
                        n++;
                }

                Console.WriteLine(n);
            }
        }
    }
}
