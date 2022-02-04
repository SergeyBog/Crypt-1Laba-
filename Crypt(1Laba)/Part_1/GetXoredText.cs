using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crypt_1Laba_.Part_1
{
   public class GetXoredText
   {
        public string GetNormalText() {
            var byteText = ConvertToBytes();
            string result = Decryption(byteText);
            return result;
        }
        
        public byte[] ConvertToBytes()
        {
            var textList = File.ReadAllText(@"C:\Users\Sergey\source\repos\Crypt(1Laba)\Crypt(1Laba)\Part_1\Task1.txt");
            int numverOfChars = textList.Length;
            byte[] bytes = new byte[numverOfChars / 2];
            for (int i = 0; i < numverOfChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(textList.Substring(i, 2), 16);
            }
            return bytes;

        }
        public string Decryption(byte[] byteArr)
        {
            string ready = "";
            var result = new string[256];
            var buffer = new byte[byteArr.Length];
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < buffer.Length; j++)
                {
                    buffer[j] = (byte)(byteArr[j] ^ i);
                }

                result[i] = Encoding.UTF8.GetString(buffer);
            }
            ready = result.OrderBy(GetEnglishText).First();
            return ready;
        }
        private List<double> EngLettersFreq = new List<double>()
        {
            0.08167, 0.01492, 0.02782, 0.04253, 0.12702,
            0.02228, 0.02015, 0.06094, 0.06966, 0.00153,
            0.00772, 0.04025, 0.02406, 0.06749, 0.07507,
            0.01929, 0.00095, 0.05987, 0.06327, 0.09056,
            0.02758, 0.00978, 0.02360, 0.00150, 0.01974,
            0.00074
        };
        private double[] Letters_Amount = new double[26];
        public double GetEnglishText(string text)
        {
            string validChars = "[\\s-.,_()“”]";
            string cleanText = Regex.Replace(text, validChars, "");
            for (int i = 0; i < Letters_Amount.Length; i++)
            {
                Letters_Amount[i] = 0;
            }
            for (int j = 0; j < cleanText.Length; j++) {
                if (cleanText[j] >= 97 && cleanText[j] <= 122)
                {
                    Letters_Amount[cleanText[j] - 97]++;
                }
                
            }
            double result = 0;
            for (int i = 0; i < 26; i++) {
                double cheked = Letters_Amount[i];
                double expected = cleanText.Length * EngLettersFreq[i];
                double differring = cheked - expected;
                result += differring * differring / expected;
            }
            return result;
        }


    }
}
