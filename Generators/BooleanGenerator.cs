using System;
using System.Collections.Generic;
using System.Text;

namespace Generators
{
    class BooleanGenerator : Generator
    {
        public BooleanGenerator(Random random) : base(random)
        {
            
        }

        public override object GenerateValue()
        {
            return Random.Next(0, 1) == 1 ? true : false; 
        }
    }
}
