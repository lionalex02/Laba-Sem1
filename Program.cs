using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kod
{
    internal class Tester
    {
        public static void Main()
        {
            string input = Console.ReadLine().Replace(" ", "");
            List<string> numbers = new List<string>();
            List<char> operations = new List<char>();


            string number = "";

            for (int i = 0; i < input.Length; i++)
            {

                if (input[i] == '+' || input[i] == '-' || input[i] == '*' || input[i] == '/')
                {
                    numbers.Add(number);
                    operations.Add(input[i]);
                    number = "";
                }
                else
                {
                    number += input[i];
                }
            }

            numbers.Add(number);

            Console.WriteLine(string.Join(" ", numbers));
            Console.WriteLine(string.Join(" ", operations));

        }
    }
}