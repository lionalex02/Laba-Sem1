using RPN.Logic.Clases;
using RPN.Logic.Clases.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace RPN.Logic
{
    public class Calculator
    {
       

        private static List<Operation> _existOperation;

        private static Operation FindeOperation(string name)
        {
            if (_existOperation == null)
            {
                Type parent = typeof(Operation);
                IEnumerable<Assembly> allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                IEnumerable<Type> types = allAssemblies.SelectMany(x => x.GetTypes());
                IEnumerable<Type> inherTypes = types.Where(t => parent.IsAssignableFrom(t) && !t.IsAbstract).ToList();

                _existOperation = inherTypes.Select(type => (Operation)Activator.CreateInstance(type)).ToList();
            }
            return _existOperation.FirstOrDefault(op => op.Name.Equals(name));
        }
        public double Resulting(string input, double x)
        {
            List<Token> postPars = Parsing(input);
            List<Token> rpn = PolishChange(postPars);
            double absoluteFinale = Calculating(rpn, x);
            return absoluteFinale;
        }
        private List<Token> Parsing(string input)
        {
            List<Token> finals = new();
            string number = "";
            string opers = "";
            foreach (char tokens in input)
            {
                if (tokens.Equals('('))
                {
                    Clearing(number, opers, finals, tokens);
                    opers = "";
                    Parenthesis par = new();
                    par.bracket = true;
                    finals.Add(par);
                    number = "";
                }
                else if (tokens.Equals(')'))
                {
                    Clearing(number, opers, finals, tokens);
                    opers = "";
                    Parenthesis par = new();
                    par.bracket = false;
                    finals.Add(par);
                    number = "";
                }
                else if (tokens.Equals(';'))
                {
                    Clearing(number, opers, finals, tokens);
                    opers = "";
                    Other obj = new();
                    finals.Add(obj);
                    number = "";
                }
                else if (tokens.Equals('x'))
                {
                    Clearing(number, opers, finals, tokens);
                    opers = "";
                    Variable digits = new Variable(tokens);
                    finals.Add(digits);
                    number = "";
                }
                else if (char.IsLetter(tokens))
                {

                    opers += tokens;
                }
                else if (char.IsDigit(tokens) ^ tokens.Equals(','))
                {
                    Clearing("", opers, finals, tokens);
                    opers = "";
                    number += tokens;
                }
                
                else
                {
                    Clearing(number, "", finals, tokens);
                    finals.Add(FindeOperation(tokens.ToString()));
                    number = "";
                    opers = "";
                }
            }
           Clearing(number, opers, finals, ' ');
            return finals;
        }

        private void Clearing(string number, string opers, List<Token> finals, char tokens)
        {
            if (number != "")
            {
                Number digits = new Number(tokens);
                digits.Numbering = double.Parse(number);
                finals.Add(digits);
            }
            if (opers != "")
            {
                finals.Add(FindeOperation(opers));

            }
        }

        private List<Token> PolishChange(List<Token> postPars)
        {
            Stack<Token> boofer = new();
            List<Token> finals = new();
            foreach (Token tokens in postPars)
            {
                if (tokens is Operation )
                {
                    if (boofer.Count == 0 || !(boofer.Peek() is Operation))
                    {
                        boofer.Push(tokens);
                        continue;
                    }

                    var op = tokens as Operation;
                    var op1 = boofer.Peek() as Operation;
                    

                    if (boofer.Count() > 0 && op1.Priority >= op.Priority)
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
                else if (tokens is Number or Variable)
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

        private double Calculating(List<Token> rpn, double x)
        {
            Stack<double> boofer = new Stack<double>();
            foreach (Token tokens in rpn)
            {
                
                if (tokens is Number num)
                {
                    boofer.Push(num.Numbering);
                }
                else if (tokens is Variable) 
                {
                    boofer.Push(x);
                }
                else if (tokens is Operation)
                {
                    Operation op = (Operation)tokens;

                    double[] args = new double[op.ArgsCount];
                    for (int i = op.ArgsCount - 1; i >= 0; i--)
                    {
                        if (boofer.Count == 0)
                            args[i] = Double.PositiveInfinity;
                        else
                            args[i] = boofer.Pop();
                    }
                    double result = ExecuteDouble(op, args);
                    boofer.Push(result);
                }
                
            }
            return boofer.Peek();
        }
        private double ExecuteDouble(Operation op, double[] args)
        {
            Number[] numbers = new Number[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                numbers[i] = new Number(args[i]);
            }
            Number resultNumber = op.Execute(numbers);
            return resultNumber.Numbering;
        }
    }
}
