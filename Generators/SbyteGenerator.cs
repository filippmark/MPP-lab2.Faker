using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Generators
{
    class SbyteGenerator : Generator
    {
        public SbyteGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(sbyte);
        }

        public override object GenerateValue(Func<Type, object> generate)
        {
            return (sbyte)Random.Next();
        }
    }
}
