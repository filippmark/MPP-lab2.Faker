using System;
using System.Collections.Generic;
using System.Text;

namespace Generators
{
    class SbyteGenerator : Generator
    {
        public SbyteGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(sbyte);
        }

        public override object GenerateValue()
        {
            return (sbyte)Random.Next();
        }
    }
}
