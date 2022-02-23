using Crypt_1Laba_.Part_0;
using Crypt_1Laba_.Part_1;
using Crypt_1Laba_.Part_2;
using Crypt_1Laba_.Part_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypt_1Laba_
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(FirstPart());
            // Console.WriteLine(SecondPart());
            // Console.WriteLine(ThirdPart());
            //Console.WriteLine(ForthPart());
            Console.ReadKey();
        }
        public static string FirstPart()
        {
            var worker = new GetText();
            string result = worker.GetCorrectString();
            return result;
        }
        public static string SecondPart()
        {
            var worker = new GetXoredText();
            string result = worker.GetNormalText();
            return result;
        }

        public static string ThirdPart()
        {
            var worker = new GetKeyXor();
            string result = worker.GetNormalText();
            return result;
        }
        public static string ForthPart()
        {
            var worker = new GeneticAlgo();
            string result = worker.GetNormalText();
            return result;
        }
     }
}
