using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Logic.Clases.Operations
{
    internal class Plus : Operation
    {
        public override string Name => "+";
        public override int Priority => 1;
        public override bool IsFunction => false;
        public override int ArgsCount => 2;
        public override Number Execute(params Number[] numbers)
        { 
            double firstNum = numbers[0].Numbering;
            double secondNum = numbers[1].Numbering;
            Number num = new Number(firstNum + secondNum);
            return num;




        }
    }
}
