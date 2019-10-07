using System;

namespace Generators
{
    class FloatGenerator : Generator
    {
        public FloatGenerator(Random random) : base(random)
        {
        }

        public override object GenerateValue()
        {
            return (float)Random.NextDouble();
        }
    }
}
