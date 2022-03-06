using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crypt_1Laba_.Part_3
{
    public class GeneticAlgo
    {
        private char[] ciphertext;
        private Random rng = new Random();
        private List<string> threeGram_word = new List<string>();
        private List<double> threeGram_number = new List<double>();
        private char[] newAlphabet;
        private double IdealIndex = 0.00097;
        private char[] Letters =
        {
            'A', 'B', 'C', 'D', 'E', 'F',
            'G', 'H', 'I', 'J', 'K', 'L', 
            'M', 'N', 'O', 'P', 'Q', 'R', 
            'S', 'T', 'U', 'V', 'W', 'X', 
            'Y', 'Z'
        };
        public string GetNormalText()
        {
            ReadAllInfo();
            List<char[]> generatedAlphabets = GetNewAlphabets(500);
            int tries = 0;
            double currentIndex = 0;
            while (currentIndex < IdealIndex)
            {
                generatedAlphabets = GetBestAlphabets(generatedAlphabets, 500);
                CrossingPopulation(generatedAlphabets);
                MutatePopulation(generatedAlphabets);
                char[] bestAlphabet = GetBestAlphabets(generatedAlphabets, 1)[0];
                currentIndex = CalculateBasedOnTreeGrams(bestAlphabet);
                tries++;
                Console.WriteLine($"\ntry: {tries}; alphabet: {new string(bestAlphabet)}; current estimation: {currentIndex * 1000}");
                Console.WriteLine(DecryptText(ciphertext, bestAlphabet));
            }
            string normalText = DecryptText(ciphertext, GetBestAlphabets(generatedAlphabets, 1)[0]);
            return normalText;
        }
        private void ReadAllInfo()
        {
            ciphertext = File.ReadAllText(@"C:\Users\Sergey\source\repos\Crypt(1Laba)\Crypt(1Laba)\Part_3\Task3.txt").ToCharArray();
            newAlphabet = new char[ciphertext.Length-2];
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
                numbers.Add(Convert.ToDouble(number));
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
            for (int i = 0; i < amount; i++)
            {
                char[] alphabet = GetAlphabet();
                alphabetList.Add(alphabet);
            }

            return alphabetList;
        }
        private char[] GetAlphabet()
        {
           char[] result = new char[Letters.Length];
           List<char> letters = Letters.ToList();
            for (int i = 0; i < Letters.Length; i++)
            {
                char letter = letters[rng.Next(0, letters.Count)];
                result[i] = letter;
                letters.Remove(letter);
            }
            return result;
        }
        private List<char[]> GetBestAlphabets(List<char[]> population , int amount)
        {
            List<char[]> sortedList = population.Distinct().OrderByDescending(CalculateBasedOnTreeGrams).ToList();
            List<char[]> resultList = new List<char[]>(amount);
            for (int i = 0; i < amount; i++)
            {
                resultList.Add(sortedList[i]);
            }
            return resultList;
        }
        private double CalculateBasedOnTreeGrams(char[] populationItem)
        {
            string decrypted = DecryptText(ciphertext, populationItem);
            return CalculateTreeGrammValue(decrypted);
        }
        private string DecryptText(char[] cipherText, char[] key)
        {
            for (int i = 0; i < cipherText.Length-2; i++)
            {
                char c = cipherText[i];
                newAlphabet[i] = Letters[Array.IndexOf(key, c)];
            }
            return new string(newAlphabet);
        }
        private double CalculateTreeGrammValue(string text)
        {
            double value = 0;
            for (var i = 0; i < text.Length - 2; i++)
            {
                string threeGramm = text.Substring(i, 3);
                int kiko = Array.IndexOf(threeGram_word.ToArray(), threeGramm);
                value += threeGram_number[kiko]; 
            }
            return value / (text.Length - 2);
        }
        private void MutatePopulation(List<char[]> population)
        {
            List<char[]> resList = new List<char[]>();
            for (int i = 0; i < population.Count; i++)
            {
                int mutateCounter = rng.Next(1, 11);
                if (mutateCounter <= 6)
                {
                  resList.Add(Mutate(population[i]));
                }
            }
            population.AddRange(resList);
        }

        public char[] Mutate(char[] alphabet)
        {
            char[] newAlphabet = (char[])alphabet.Clone();
            for (int i = 0; i < rng.Next(2) + 1; i++)
            {
                int number1 = rng.Next(newAlphabet.Length);
                int number2 = rng.Next(newAlphabet.Length);
                char buffer1 = newAlphabet[number1];
                char buffer2 = newAlphabet[number2];
                newAlphabet[number1] = buffer2;
                newAlphabet[number2] = buffer1;
            }
            return newAlphabet;
        }
        private void CrossingPopulation(List<char[]> alphabetList)
        {
            List<char[]> childAlphabet = new List<char[]>();
            Random rng = new Random();
            for (int i = 1; i < alphabetList.Count * 2; i++)
            {
                int number1 = rng.Next(alphabetList.Count);
                int number2 = rng.Next(alphabetList.Count);
                childAlphabet.Add(Cross(alphabetList[number1], alphabetList[number2]));
            }
            alphabetList.AddRange(childAlphabet);
        }
        public char[] Cross(char[] firstAlphabet, char[] secondAlphabet)
        {
            char[] crossArray = new char[firstAlphabet.Length];
            List<char> letters = Letters.ToList();

            for (int i = 0; i < firstAlphabet.Length; i++)
            {
                char firstAlphabetChar = firstAlphabet[i];
                char secondAlphabetChar = secondAlphabet[i];
                bool ContainFirstAlphabet = crossArray.Contains(firstAlphabetChar);
                bool ContaintSecondAlphabet = crossArray.Contains(secondAlphabetChar);

                if (ContainFirstAlphabet && ContaintSecondAlphabet)
                {
                    char randomLetter = letters[rng.Next(0, letters.Count)];
                    crossArray[i] = randomLetter;
                    letters.Remove(randomLetter);
                }
                else if (ContainFirstAlphabet)
                {
                    crossArray[i] = secondAlphabetChar;
                    letters.Remove(secondAlphabetChar);
                }
                else if (ContaintSecondAlphabet)
                {
                    crossArray[i] = firstAlphabetChar;
                    letters.Remove(firstAlphabetChar);
                }
                else if (firstAlphabetChar == secondAlphabetChar)
                {
                    crossArray[i] = firstAlphabetChar;
                    letters.Remove(firstAlphabetChar);
                }
                else
                {
                    int roflik = rng.Next(1, 3);
                    if (roflik == 1)
                    {
                        crossArray[i] = firstAlphabetChar;
                        letters.Remove(firstAlphabetChar);
                    }
                    else if (roflik == 2) {
                        crossArray[i] = secondAlphabetChar;
                        letters.Remove(secondAlphabetChar);
                    }
                }
                
            }

            return crossArray;
        }
    }

}
