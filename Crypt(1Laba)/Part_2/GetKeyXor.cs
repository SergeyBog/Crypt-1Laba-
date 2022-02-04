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
            CalculateKeyLength(text);
        }
        private void CalculateKeyLength(byte[] str)
        {

            int[] posibleKeyLenght = new int[str.Length];
            for (int smech = 1; smech < str.Length; smech++)
            {
                int counter = 0;
                for (int ind = 0; ind < str.Length; ind++)
                {
                    int newInd = (ind + smech) % str.Length;
                    if (str[ind] == str[newInd])
                    {
                        counter++;
                    }
                }
                posibleKeyLenght[smech] = counter;
            }
            Console.WriteLine(posibleKeyLenght.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key);


        }
    }
}
