using System;
using System.Collections.Generic;
using System.Text;

namespace Generators
{
    public class IntGenerator : IGenerator
    {
        public object GenerateValue()
        {
            Random random = new Random();
            return random.Next();
        }
    }
}