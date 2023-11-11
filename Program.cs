using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoplessProject
{
    internal class Tester
    {
        static void Main()
        {
            string input = Console.ReadLine().Replace(" ", "");
            List<double> numbers = new List<double>();
            List<char> operations = new List<char>();

            Parsing(numbers, operations, input);
            Console.WriteLine("Выражение состоит из цифр: " + string.Join(" ", numbers));
            Console.WriteLine("Выражение состоит из операторов: " + string.Join(" ", operations));
            Сomputation(numbers, operations);

            Console.WriteLine("Итог выражения: " + string.Join(" ", numbers));
           
        }

        public static void Parsing(List<double> numbers, List<char> operations, string input)
        {

            string number = "";
            for (int i = 0; i < input.Length; i++)
            {

                if (!char.IsDigit(input[i]))
                {
                    numbers.Add(double.Parse(number));
                    operations.Add(input[i]);
                    number = "";
                }
                else
                {
                    number += input[i];
                }
            }
            numbers.Add(double.Parse(number));
        }

        public static void Сomputation(List<double> numbers, List<char> operations)
        {
            while (operations.Count > 0)
            {

                if (operations.Contains('*') || operations.Contains('/'))
                {
                    int mult = operations.IndexOf('*');
                    int divis = operations.IndexOf('/');
                    int doer;

                    if (mult == -1) doer = divis;
                    else if (divis == -1) doer = mult;
                    else doer = Math.Min(divis, mult);
                    
                    double subsequent = GetNumber(numbers[doer], numbers[doer + 1], operations[doer]);

                    numbers.RemoveAt(doer +1);
                    numbers.RemoveAt(doer);
                    operations.RemoveAt(doer);

                    numbers.Insert(doer, subsequent);
                }
                else if (operations.Contains('+') || operations.Contains('-'))
                {
                    double subsequent = GetNumber(numbers[0], numbers[1], operations[0]);

                    numbers.RemoveAt(1);
                    numbers.RemoveAt(0);
                    operations.RemoveAt(0);

                    numbers.Insert(0, subsequent);
                }
            }
        }

        public static double GetNumber(double number1, double number2, char operation)
        {
            switch (operation)
            {
                case '+': return 
                        number1 + number2;
                case '-': return 
                        number1 - number2;
                case '*': return 
                        number1 * number2;
                case '/': return 
                        number1 / number2;
                default: return 0;
            }
        }
    }   
}