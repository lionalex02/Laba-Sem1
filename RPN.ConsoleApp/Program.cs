using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RPN.Logic;
using RPN.Logic.Clases;




namespace RPN.ConsoleApp
{

    public class Program
    {
        public static void Main()
        {
            Console.Write("Введите значение переменной x: ");
            //double x = 10.0;
            double x = Convert.ToDouble(Console.ReadLine().Replace(".", ","));
            Console.Write("Введите выражение: ");
            string inpt = Console.ReadLine().ToLower().Replace(".", ",");
            var calculator = new Calculator();
            double result = calculator.Resulting(inpt, x);
            //Console.Write("Выражение в виде ОПЗ: ");
            //calculator.Print(rpn);
            Console.WriteLine("Итог выражения: " + string.Join(" ", result));
        }

    }
}
