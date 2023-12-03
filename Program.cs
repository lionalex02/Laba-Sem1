using System;
using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HoplessProject
{
    public class Token { }

    public class Number : Token
    {
        public double number;
    }

    public class Operation : Token
    {
        public char operation;
    }

    public class Parenthesis : Token
    {
        public bool bracket;
    }
    public class Tester
    {
        public static void Main()
        {
            Console.Write("Введите выражение: ");
            string input = Console.ReadLine()!.Replace("\t", " ").Replace(" ", "");
            List<Token> postPars = Parsing(input);
            List<Token> rpn = PolishChange(postPars);
            double absoluteFinale = Calculating(rpn);
            Console.Write("Выражение в виде ОПЗ: ");
            Print(rpn, new(), new(), new());
            Console.WriteLine("Итог выражения: " + string.Join(" ", absoluteFinale));
        }
        public static List<Token> Parsing(string input)
        {
            List<Token> finals = new();
            string number = "";
            foreach (char tokens in input)
            {
                if (tokens != ' ')
                {
                   if (tokens.Equals('('))
                    {
                        Parenthesis par = new();
                        par.bracket = true;
                        finals.Add(par);
                    }
                    else if (tokens.Equals(')'))
                    {
                        Parenthesis par = new();
                        par.bracket = false;
                        finals.Add(par);
                    }
                    else if (tokens.Equals('+') ^ tokens.Equals('-') ^ tokens.Equals('*') ^ tokens.Equals('/'))
                    {
                        if (number != "")
                        {
                            Number digits = new();
                            digits.number = Convert.ToDouble(number);
                            finals.Add(digits);
                        } 
                        Operation op = new();
                        op.operation = tokens;
                        finals.Add(op);
                        number = "";
                    }
                    else 
                        number += tokens;
                }
            }
            if (number != "")
            {
                Number digits = new();
                digits.number = Convert.ToDouble(number);
                finals.Add(digits);
            }
            return finals;
        }
        public static double PriorityOper(Token priority)
        {
            if (priority is Operation)
            {
                return ((Operation)priority).operation switch
                {
                    '+' or '-' => 1,
                    '*' or '/' => 2,
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
                Console.WriteLine(tokens);
                if (tokens is Operation)
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
                    finals.Add(tokens);
            }
            while (boofer.Count() > 0)
            {
                finals.Add(boofer.Pop());
            }
            return finals;
        }


        public static double GetNumber(Number number1, Number number2, Operation priority)
        {
                return ((Operation)priority).operation switch
                {
                    '+' => number1.number + number2.number,
                    '-' => number1.number - number2.number,
                    '*' => number1.number * number2.number,
                    '/' => number1.number / number2.number,
                    _ => 0.0,
                };
        }

        public static double Calculating(List<Token> rpn)
        {
            Stack<double> boofer = new();
            foreach (Token tokens in rpn)
            {

                if (tokens is Operation)
                {
                    Number firstNum = new();
                    Number secondNum = new();
                    double number2 = boofer.Pop();
                    double number1 = boofer.Pop();
                    firstNum.number = number1;
                    secondNum.number = number2;
                    double priority = GetNumber((Number)firstNum, (Number)secondNum, (Operation)tokens);
                    boofer.Push(priority);
                }
                else if (tokens is Number numberpup)
                    boofer.Push(numberpup.number);
            }
            return boofer.Peek();
        }


        static void Print(List<Token> ListToPrint, Number number, Operation op, Parenthesis bracket)
        {
            foreach (Token bringOut in ListToPrint)
            {
                if (bringOut is Number)
                {
                    number = (Number)bringOut;
                    Console.Write(number.number);
                    Console.Write(" ");
                }
                else if (bringOut is Operation)
                {
                    op = (Operation)bringOut;
                    Console.Write(op.operation);
                    Console.Write(" ");
                }
                else
                {
                    bracket = (Parenthesis)bringOut;
                    if (bracket.bracket) Console.Write("( ");
                    else Console.Write(") ");
                }
            }
            Console.Write("\n");
        }

    }
}