using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypt_1Laba_.Part_0
{
    public class GetText
    {
        public string GetCorrectString()
        {
            var textList = File.ReadAllText(@"C:\Users\Sergey\source\repos\Crypt(1Laba)\Crypt(1Laba)\Part_0\Task0.txt");
            int numverOfChars = textList.Length;
            string result = "";
            List<string> binaryCode = new List<string>();
            for (int i = 0; i < textList.Length; i++)
            {
                result += textList[i];
                if (result.Length % 8 == 0)
                {
                    binaryCode.Add(result);
                    result = "";
                }
            }
            result = "";
            int[] rofl = new int[binaryCode.Count];
            for (int j = 0; j < binaryCode.Count; j++)
            {
                rofl[j] = Convert.ToInt32(binaryCode[j], 2);

            }
            for (int k = 0; k < rofl.Length; k++)
            {
                result += Convert.ToChar(rofl[k]);
            }
            byte[] data = Convert.FromBase64String(result);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }
    }
}
