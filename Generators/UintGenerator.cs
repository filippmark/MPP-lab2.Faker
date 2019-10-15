using System;
using System.Collections.Generic;
using System.Text;

namespace Generators
{
    class UintGenerator : Generator
    {
        public UintGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(uint);
        }

        public override object GenerateValue()
        {
            byte[] buf = new byte[4];
            Random.NextBytes(buf);
            return BitConverter.ToUInt32(buf, 0);
        }
    }
}
