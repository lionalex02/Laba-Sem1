using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPN.Logic.Clases
{
    abstract class Operation : Token
    {
        public char operation;
        public abstract string Name { get; }
        public abstract int Priority { get; }
        public abstract bool IsFunction { get; }
        public abstract int ArgsCount { get; }
        public abstract Number Execute(params Number[] numbers);

    }
}
