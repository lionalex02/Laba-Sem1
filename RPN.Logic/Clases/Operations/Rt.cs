using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Logic.Clases.Operations
{
    internal class Rt : Operation
    {
        public override string Name => "rt";
        public override int Priority => 4;
        public override bool IsFunction => true;
        public override int ArgsCount => 2;
        public override Number Execute(params Number[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i].Numbering == Double.PositiveInfinity)
                {
                    numbers[i].Numbering = 2.0;
                    break;
                }

            }
            double firstNum = numbers[1].Numbering;
            double secondNum = numbers[0].Numbering;
            return new Number(Math.Pow(firstNum, 1.0 / secondNum));
        }
    }
}
