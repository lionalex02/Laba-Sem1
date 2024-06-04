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
            //Print(res);
            Console.WriteLine("Итог выражения: " + string.Join(" ", result));
        }
        //private void Print(List<Token> tokens)
        //{
        //    foreach (Token token in tokens)
        //    {
        //        if (token is Number num)
        //        {
        //            Console.Write(num.Numbering + " ");
        //        }
        //        else if (token is Variable var)
        //        {
        //            Console.Write(var.variable + " ");

        //        }
        //        else if (token is Operation op)
        //        {
        //            Console.Write(op.Name + " ");
        //        }
        //        else if (token is Parenthesis par)
        //        {
        //            Console.Write(par.bracket ? "( " : ") ");
        //        }
        //    }
        //    Console.Write("\n");
        //}

    }
}
