using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Generators
{
    class UlongGenerator : Generator
    {
        public UlongGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(ulong);
        }

        public override object GenerateValue(Func<Type, object> generate)
        {
            byte[] buf = new byte[8];
            Random.NextBytes(buf);
            return BitConverter.ToUInt64(buf, 0);
        }
    }
}
