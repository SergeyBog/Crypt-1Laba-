using Crypt_1Laba_.Part_0;
using Crypt_1Laba_.Part_1;
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
            Console.WriteLine(SecondPart());
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
    }
}
