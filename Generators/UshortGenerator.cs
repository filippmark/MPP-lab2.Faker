using System;
using System.Collections.Generic;
using System.Text;

namespace Generators
{
    class UshortGenerator : Generator
    {
        public UshortGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(ushort);
        }

        public override object GenerateValue()
        {
            byte[] bytes = new byte[2];
            Random.NextBytes(bytes);
            return BitConverter.ToUInt16(bytes);
        }
    }
}
