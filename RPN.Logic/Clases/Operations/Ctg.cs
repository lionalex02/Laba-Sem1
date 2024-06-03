using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Logic.Clases.Operations
{
    internal class Ctg : Operation
    {
        public override string Name => "ctg";
        public override int Priority => 4;
        public override bool IsFunction => true;
        public override int ArgsCount => 1;
        public override Number Execute(params Number[] numbers)
        {
            double firstNum = numbers[0].Numbering;
            return new Number(1.0 / Math.Tan(firstNum));
        }
    }
}

