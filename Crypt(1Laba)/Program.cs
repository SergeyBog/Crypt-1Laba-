using Crypt_1Laba_.Part_0;
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
            Console.WriteLine(FirstPart());
            Console.ReadKey();
        }
        public static string FirstPart()
        {
            var worker = new GetText();
            string result = worker.GetCorrectString();
            return result;
        }
    }
}
