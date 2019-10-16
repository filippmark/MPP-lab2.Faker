using System;
using System.Reflection;

namespace Generators
{
    public class IntGenerator : Generator
    {
        public IntGenerator(Random random) : base(random)
        {
            GeneratedType = typeof(int);
        }

        public override object GenerateValue(Func<Type, object> generate)
        {
            return Random.Next();
        }
    }
}