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
           // string encodeText = Encoding.ASCII.GetString(text);
           // FindLenghtOfKey(encodeText);
            int keyLenght = 3;
            return DecryptedText(keyLenght, text);
        }

        public string DecryptedText(int keyLenght,byte[] text)
        {
            var kiko = GetDevidedSectionBykey(keyLenght, text);
            var worker = new GetXoredText();
            List<string> decryptedSections = new List<string>();

            foreach (List<byte> element in kiko)
            {
                string rofl = worker.Decryption(element.ToArray());
                decryptedSections.Add(rofl);
            }
            var result = WorkWithSection(decryptedSections, keyLenght);
            return result;
        }   
        public List<List<byte>>GetDevidedSectionBykey(int length, byte[] text)
        {
            List<List<byte>> result = new List<List<byte>>();

            for (int i = 0; i < length; i++)
            {
                var resik = new List<byte>();
                for (int j = i; j < text.Length; j += length)
                {
                    resik.Add(text[j]);
                }
                result.Add(resik);
            }

            return result;
        }

        public string WorkWithSection(List<string> section, int keyLength)
        {
            string result = "";
            int minimal = FindMinimalLenght(section);
            for (var i = 0; i < minimal; i++)
            {
                for (var j = 0; j < keyLength; j++)
                {
                    result += section[j][i];
                }
            }
            for (int k = 0; k < section.Count; k++)
            {
                if (section[k].Length > minimal)
                {
                    result += section[k][minimal];
                }
            }
            return result;
        }
        public int FindMinimalLenght(List<string> list)
        {
            int counter = 0;
            foreach (string s in list)
            {
                if (s.Length>counter)
                {
                    counter = s.Length;
                }
            }
            return counter - 1;

        }

       /* public void FindLenghtOfKey(string encodedText)
        {
            for (int i = 0; i < encodedText.Length; i++)
            {
                var number = 0;
                var workText = encodedText.Substring(encodedText.Length - i, i) + encodedText.Substring(0, encodedText.Length - i);
                for (int j = 0; j < encodedText.Length; j++)
                {
                    if (workText[j] == encodedText[j])
                        number++;
                }
                Console.WriteLine(number);
            }
        }*/
    }
}
