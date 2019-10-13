using System;

namespace Generators
{
    public class IntGenerator : Generator
    {
        public IntGenerator(Random random) : base(random)
        {
            GeneratedType = typeof(int);
        }

        public override object GenerateValue()
        {
            return Random.Next();
        }
    }
}