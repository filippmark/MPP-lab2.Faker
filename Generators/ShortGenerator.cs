using System;
using System.Collections.Generic;
using System.Text;

namespace Generators
{
    class ShortGenerator : Generator
    {
        public ShortGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(short);
        }

        public override object GenerateValue()
        {
            return (short)Random.Next();
        }
    }
}
