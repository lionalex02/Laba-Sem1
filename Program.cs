using System;
using System.Collections.Generic;



namespace HoplessProject
{
    internal class Tester
    {
        static void Main()
        {
            Console.Write("Введите выражение: ");
            string input = Console.ReadLine().Replace("\t", " ").Replace(" ", "");
            List<object> postPars = Parsing(input);
            List<object> rpn = KurwaChange(postPars);
            List<object> absoluteFinale = Calculating(rpn);
            Console.WriteLine("Выражение в виде ОПЗ: " + string.Join(" ", rpn));
            Console.WriteLine("Итог выражения: " + string.Join(" ", absoluteFinale));
        }
        public static List<object> Parsing(string input)
        {
            List<object> finals = new();
            List<double> numbersOut = new();
            string number = "";
            foreach (char tokens in input)
            {
                if (tokens != ' ')
                {
                    if (!char.IsDigit(tokens))
                    {
                        if (number != "")
                        {
                            finals.Add(number);
                            numbersOut.Add(double.Parse(number));
                        }
                        finals.Add(tokens);
                        number = "";
                    }
                    else
                        number += tokens;
                }
            }
            if (number != "")
            {
                finals.Add(number);
                numbersOut.Add(double.Parse(number));
            }
            Console.WriteLine("Выражение состоит из цифр: " + string.Join(" ", numbersOut));
            return finals;
        }
        static int PriorityOper(object priority)
        {
            return priority switch
            {
                '+' or '-' => 1,
                '*' or '/' => 2,
                _ => 0,
            };
        }

        public static List<object> KurwaChange(List<object> postPars)
        {
            Stack<object> boofer = new();
            List<object> finals = new();
            List<object> operant = new();
            foreach (object tokens in postPars)
            {
                if (tokens.Equals('+') ^ tokens.Equals('-') ^ tokens.Equals('*') ^ tokens.Equals('/'))
                {
                    if (boofer.Count() > 0 && PriorityOper(boofer.Peek()) >= PriorityOper(tokens))
                    {
                        operant.Add(boofer.Peek());
                        finals.Add(boofer.Pop());
                    }
                    boofer.Push(tokens);
                }
                else if (tokens.Equals('('))
                    boofer.Push(tokens);

                else if (tokens.Equals(')'))
                {
                    while (!boofer.Peek().Equals('('))
                    {
                        finals.Add(boofer.Pop());
                    }
                    boofer.Pop();
                }
                else if (tokens is string)
                    finals.Add(tokens);

            }
            while (boofer.Count() > 0)
            {
                operant.Add(boofer.Peek());
                finals.Add(boofer.Pop());
            }
            Console.WriteLine("Выражение состоит из операторов: " + string.Join(" ", operant));
            return finals;
        }
        public static double GetNumber(double number1, double number2, object priority)
        {
            return priority switch
            {
                '+' => number1 + number2,
                '-' => number1 - number2,
                '*' => number1 * number2,
                '/' => number1 / number2,
                _ => 0
            };
        }
        public static List<object> Calculating(List<object> rpn)
        {
            Stack<object> boofer = new();
            List<object> finals = new();
            List<object> TEST = new();
            foreach (object tokens in rpn)
            {
                if (tokens.Equals('+') ^ tokens.Equals('-') ^ tokens.Equals('*') ^ tokens.Equals('/'))
                {
                    double number2 = Convert.ToDouble(boofer.Pop());
                    double number1 = Convert.ToDouble(boofer.Pop());
                    object priority = GetNumber(number1, number2, tokens);
                    boofer.Push(priority);
                }
                else if (tokens is string)
                    boofer.Push(tokens);
            }
            finals.Add(boofer.Peek());
            return finals;
        }
    }
}
