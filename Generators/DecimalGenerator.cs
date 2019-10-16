using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Generators
{
    public class DecimalGenerator : Generator
    {
        public DecimalGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(decimal);
        }

        public override object GenerateValue(Func<Type, object> generate)
        {
            return NextDecimal();
        }

        private int NextInt32()
        {
            int firstBits = Random.Next(0, 1 << 4) << 28;
            int lastBits = Random.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        private decimal NextDecimal()
        {
            byte scale = (byte)Random.Next(29);
            bool sign = Random.Next(2) == 1;
            return new decimal(NextInt32(),
                               NextInt32(),
                               NextInt32(),
                               sign,
                               scale);
        }
    }
}
