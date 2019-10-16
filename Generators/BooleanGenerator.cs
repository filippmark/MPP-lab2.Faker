using System;
using System.Reflection;

namespace Generators
{
    class BooleanGenerator : Generator
    {
        public BooleanGenerator(Random random) : base(random)
        {
            GeneratedType = typeof(bool);
        }

        public override object GenerateValue(Func<Type, object> generate)
        {
            return true;
        }
    }
}