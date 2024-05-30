using HoplessProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace HoplessProject
{
    public class Token { }

    public class Number : Token
    {
        public double number;
        public bool isVariable;
    }

    public class Operation : Token
    {
        public char operation;
    }
    public class Function : Token
    {
        public char function;
    }
    public class Other : Token
    {
        public char other;
    }
    public class Parenthesis : Token
    {
        public bool bracket;
    }

    public class Program
    {
        public static void Main()
        {
            Console.Write("Введите значение переменной x: ");
            double x = 10.0;
            // double x = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите выражение: ");
            string input = Console.ReadLine()!.ToLower()
                .Replace("\t", " ")
                .Replace(" ", "")
                .Replace(".", ",")
                .Replace("log", "l")
                .Replace("sqrt", "q")
                .Replace("rt", "r")
                .Replace("sin", "s")
                .Replace("cos", "c")
                .Replace("tg", "t")
                .Replace("ctg", "g")
                .Replace("asin", "a")
                .Replace("abs", "b")
                .Replace("ln", "n")
                .Replace("lg", "i");

            List<Token> postPars = Parsing(input);
            Print(postPars);
            List<Token> rpn = PolishChange(postPars);
            Print(rpn);
            double absoluteFinale = Calculating(rpn, x);

            Console.Write("Выражение в виде ОПЗ: ");
            Print(rpn);
            Console.WriteLine("Итог выражения: " + string.Join(" ", absoluteFinale));
        }

        public static List<Token> Parsing(string input)
        {
            List<Token> finals = new();
            string number = "";
            foreach (char tokens in input)
            {
                if (tokens.Equals('('))
                {
                    Clearing(number, finals);
                    Parenthesis par = new();
                    par.bracket = true;
                    finals.Add(par);
                }
                else if (tokens.Equals(')'))
                {
                    Clearing(number, finals);
                    Parenthesis par = new();
                    par.bracket = false;
                    finals.Add(par);
                    number = "";
                }
                else if (tokens.Equals(';'))
                {
                    Clearing(number, finals);
                    Other obj = new();
                    finals.Add(obj);
                    number = "";
                }
                else if (tokens.Equals('x'))
                {
                    Clearing(number, finals);
                    Number digits = new();
                    digits.isVariable = true;
                    finals.Add(digits);
                    number = "";
                }
                else if (tokens.Equals('+') ^ tokens.Equals('-') ^ tokens.Equals('*') ^ tokens.Equals('/') ^ tokens.Equals('^') ^ tokens.Equals('l') ^ tokens.Equals('r') )
                {
                    Clearing(number, finals);
                    Operation op = new();
                    op.operation = tokens;
                    finals.Add(op);
                    number = "";
                }
                else if ( tokens.Equals('s') ^ tokens.Equals('q') ^ tokens.Equals('t') ^ tokens.Equals('g') ^ tokens.Equals('c') ^ tokens.Equals('a') ^ tokens.Equals('b') ^ tokens.Equals('n') ^ tokens.Equals('i'))
                {
                    Clearing(number, finals);
                    Function digits = new();
                    digits.function = tokens;
                    finals.Add(digits);
                }
                else if (char.IsDigit(tokens) ^ tokens.Equals(','))
                {
                    number += tokens;
                }
                else
                    Console.WriteLine("Неизвестный символ");
            }
            Clearing(number, finals);
            return finals;
        }

        public static void Clearing(string number, List<Token> finals)
        {
            if (number != "")
            {
                Number digits = new();
                digits.number = double.Parse(number);
                finals.Add(digits);
            }
        }

        public static double PriorityOper(Token priority)
        {
            if (priority is Operation)
            {
                return ((Operation)priority).operation switch
                {
                    '+' or '-' => 1,
                    '*' or '/' or '^' => 2,
                    _ => 0
                };
            }
            else if (priority is Function)
            {
                return ((Function)priority).function switch
                {
                    'q' or 'c' or 's' or 'g' or 't' or 'a' or 'b' => 3,
                    'l' or 'r' or 'i' or 'n' => 4,
                    _ => 0
                };
            }
            return 0;
        }
        public static List<Token> PolishChange(List<Token> postPars)
        {
            Stack<Token> boofer = new();
            List<Token> finals = new();
            foreach (Token tokens in postPars)
            {
                if (tokens is Operation or Function)
                {
                    if (boofer.Count() > 0 && PriorityOper(boofer.Peek()) >= PriorityOper(tokens))
                    {
                        finals.Add(boofer.Pop());
                    }
                    boofer.Push(tokens);
                }
                else if (tokens is Parenthesis)
                {
                    if (((Parenthesis)tokens).bracket)
                    {
                        boofer.Push((Parenthesis)tokens);
                    }
                    else
                    {
                        while (boofer.Count > 0 && !(boofer.Peek() is Parenthesis))
                        {
                            finals.Add(boofer.Pop());
                        }
                        boofer.Pop();
                    }
                }
                else if (tokens is Number)
                {
                    finals.Add(tokens);
                }
                else
                    continue;
            }
            while (boofer.Count() > 0)
            {
                finals.Add(boofer.Pop());
            }
            return finals;
        }

        public static double GetNumber(Number number1, Number number2, Token priority)
        {
            if (priority is Operation)
            {
                return ((Operation)priority).operation switch
                {
                    '+' => number1.number + number2.number,
                    '-' => number1.number - number2.number,
                    '*' => number1.number * number2.number,
                    '/' => number1.number / number2.number,
                    'l' => Math.Log(number2.number, number1.number),
                    '^' => Math.Pow(number1.number,number2.number),
                    'r' => Math.Pow(number2.number, 1.0 / number1.number),
                    _ => 0.0,
                };
            }
            else
            {
                return ((Function)priority).function switch
                {
                    'q' => Math.Sqrt(number1.number),
                    's' => Math.Sin(number1.number),
                    'c' => Math.Cos(number1.number),
                    't' => Math.Tan(number1.number),
                    'g' => 1.0 / Math.Tan(number1.number),
                    'a' => Math.Asin(number1.number),
                    'b' => Math.Abs(number1.number),
                    'n' => Math.Log(number1.number),
                    'i' => Math.Log10(number1.number),
                    _ => 0.0,
                };
            }
        }

        public static double Calculating(List<Token> rpn, double x)
        {
            Stack<double> boofer = new();
            foreach (Token tokens in rpn)
            {
                Number firstNum = new();
                Number secondNum = new();
                firstNum.number = 10.0;
                if (tokens is Operation)
                {
                    double number2 = boofer.Pop();
                    if (boofer.Count >= 1)
                    {
                        double number1 = boofer.Pop();
                        firstNum.number = number1;
                    }
                    secondNum.number = number2;
                    double priority = GetNumber((Number)firstNum, (Number)secondNum, tokens);
                    boofer.Push(priority);
                }
                else if (tokens is Number)
                {
                    boofer.Push(((Number)tokens).isVariable ? x : ((Number)tokens).number);
                }
                else if (tokens is Function)
                {
                    double number1 = boofer.Pop();
                    firstNum.number = number1;
                    secondNum.number = 1.0;
                    double priority = GetNumber((Number)firstNum, (Number)secondNum, tokens);
                    boofer.Push(priority);
                }
            }
            return boofer.Peek();
        }

        public static readonly Dictionary<char, string> FunctionDict = new ()
        {
            { 'l', "log" },
            { 'q', "sqrt" },
            { 'r', "rt" },
            { 's', "sin" },
            { 'c', "cos" },
            { 't', "tg" },
            { 'g', "ctg" },
            { 'a', "asin" },
            { 'b', "abs" },
            { 'n', "ln" },
            { 'i', "lg" },
            { '+', "+" },
            { '-', "-" },
            { '/', "/" },
            { '*', "*" },
            { '^', "^" }

        };
        static void Print(List<Token> tokens)
        {
            foreach (Token token in tokens)
            {
                if (token is Number num)
                {
                    Console.Write(num.isVariable ? "x " : $"{num.number} ");
                }
                else if (token is Operation op)
                {
                    Console.Write($"{FunctionDict[op.operation]} ");
                }
                else if (token is Function func)
                {
                    Console.Write($"{FunctionDict[func.function]} ");
                }
                else if (token is Parenthesis par)
                {
                    Console.Write(par.bracket ? "( " : ") ");
                }
            }
            Console.Write("\n");
        }

    }
}
