using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Logic.Clases.Operations
{
    internal class Log : Operation
    {
        private Number zero = new Number(0);

        public override string Name => "log";
        public override int Priority => 4;
        public override bool IsFunction => true;
        public override int ArgsCount => 2;
        public override Number Execute(params Number[] numbers)
      {
           
            
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i].Numbering == Double.PositiveInfinity || numbers[i].Numbering == Double.NegativeInfinity)
                {
                    numbers[i].Numbering = 10.0;
                    continue;
                }
                else if (numbers[i].Numbering == 0)
                    return zero;
                
                
            }
            

            double firstNum = numbers[1].Numbering;
            double secondNum = numbers[0].Numbering;
            return new Number(Math.Log(firstNum, secondNum));
            
        }
    }
}
