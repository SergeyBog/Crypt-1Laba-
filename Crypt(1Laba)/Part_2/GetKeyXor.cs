using Crypt_1Laba_.Part_1;
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
       public string GetNormalText()
       {
            var base64 = File.ReadAllText(@"C:\Users\Sergey\source\repos\Crypt(1Laba)\Crypt(1Laba)\Part_2\Task2.txt");
            byte[] text = Convert.FromBase64String(base64);
            // var encodeText = Encoding.ASCII.GetString(text);
            //FindKeyLength(encodeText);
            int keyLenght = 3;
            var kiko = GetDevidedSectionBykey(keyLenght, text);
            
            var worker = new GetXoredText();
            List<string> decryptedSections = kiko.Select(section => worker.Decryption(section.ToArray())).ToList();
            var result = WorkWithSelections(decryptedSections, keyLenght);
            return result;
       }
        public void FindKeyLength(string encodedText)
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
        
        public List<List<byte>>GetDevidedSectionBykey(int length, byte[] text)
        {
            List<List<byte>> resultList = new List<List<byte>>();

            for (int i = 0; i < length; i++)
            {
                var resik = new List<byte>();
                for (int j=i;j<text.Length;j+=length)
                {
                    resik.Add(text[j]);
                }
                resultList.Add(resik);
            }

            return resultList;
        }

        public string WorkWithSelections(List<string> selections, int keyLength)
        {
            string result = "";
            for (var i = 0; i < selections.Min(str => str.Length); i++)
            {
                for (var j = 0; j < keyLength; j++)
                {
                    result += selections[j][i];
                }
            }

            var lastLetter = selections[0].Length - 1;

            for (var j = 0; j < keyLength; j++)
            {
                if (selections[j].Length == lastLetter + 1)
                {
                    result += selections[j][lastLetter];
                }
            }

            return result;
        }
    }
}
