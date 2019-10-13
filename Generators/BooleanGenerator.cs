using System;

namespace Generators
{
    class BooleanGenerator : Generator
    {
        public BooleanGenerator(Random random) : base(random)
        {
            GeneratedType = typeof(bool);
        }

        public override object GenerateValue()
        {
            return Random.Next(0, 1) == 1 ? true : false;
        }
    }
}