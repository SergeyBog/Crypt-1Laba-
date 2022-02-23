using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypt_1Laba_.Part_3
{
    public class GeneticAlgo
    {
        private char[] ciphertext;
        private Random rng = new Random();
        private List<string> threeGram_word = new List<string>();
        private List<double> threeGram_number = new List<double>();
        public char[] Letters =
         {
            'A', 'B', 'C', 'D', 'E', 'F',
            'G', 'H', 'I', 'J', 'K', 'L', 
            'M', 'N', 'O', 'P', 'Q', 'R', 
            'S', 'T', 'U', 'V', 'W', 'X', 
            'Y', 'Z'
        };
        public string GetNormalText()
        {
            string result = "";
            ReadAllInfo();
            List<char[]> generatedAlphabets = GetNewAlphabets(400);
    
            return result;
        }
        private void ReadAllInfo()
        {
            ciphertext = File.ReadAllText(@"C:\Users\Sergey\source\repos\Crypt(1Laba)\Crypt(1Laba)\Part_3\Task3.txt").ToCharArray();
            List<string> words = new List<string>();
            List<double> numbers = new List<double>();
            double sum = 0;
            string[] text = File.ReadAllLines(@"C:\Users\Sergey\source\repos\Crypt(1Laba)\Crypt(1Laba)\Part_3\3grams.txt");
            for (int i = 0; i < text.Length; i++)
            {
                string word = "";
                string number = "";
                char[] kiko = text[i].ToCharArray();
                for (int j = 0; j < kiko.Length; j++)
                {
                    if (j < 3)
                    {
                        word += kiko[j];
                    }
                    else if (j == 3)
                    {

                    }
                    else
                    {
                        number += kiko[j];
                    }
                }
                sum += Convert.ToDouble(number);
                words.Add(word.ToUpper());
                word = "";
                numbers.Add(Convert.ToDouble(number));
                number = "";
            }
            for (int k = 0; k < numbers.Count; k++)
            {
                double amount = numbers[k] / sum;
                threeGram_word.Add(words[k]);
                threeGram_number.Add(amount);
            }
        }
        private List<char[]> GetNewAlphabets(int amount)
        {
            List<char[]>alphabetList = new List<char[]>();
            for (var i = 0; i < amount; i++)
            {
                var alphabet = GetAlphabet();
                alphabetList.Add(alphabet);
            }

            return alphabetList;
        }
        private char[] GetAlphabet()
        {
           char[] result = new char[Letters.Length];
           List<char> letters = Letters.ToList();
            for (var i = 0; i < Letters.Length; i++)
            {
                char letter = letters[rng.Next(0, letters.Count)];
                result[i] = letter;
                letters.Remove(letter);
            }
            return result;
        }
       

    }

}
