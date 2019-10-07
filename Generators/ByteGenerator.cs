using System;

namespace Generators
{
    class ByteGenerator : Generator
    {
        public ByteGenerator(Random random) : base(random)
        {

        }

        public override object GenerateValue()
        {
            return (byte)Random.Next();
        }
    }
}
