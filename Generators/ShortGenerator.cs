using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Generators
{
    class ShortGenerator : Generator
    {
        public ShortGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(short);
        }

        public override object GenerateValue(Func<Type, object> generate)
        {
            return (short)Random.Next();
        }
    }
}
