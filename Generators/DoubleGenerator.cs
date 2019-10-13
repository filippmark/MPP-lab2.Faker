using System;

namespace Generators
{
    class DoubleGenerator : Generator
    {
        public DoubleGenerator(Random random) : base(random)
        {
            GeneratedType = typeof(double);
        }

        public override object GenerateValue()
        {
            return Random.NextDouble();
        }
    }
}
