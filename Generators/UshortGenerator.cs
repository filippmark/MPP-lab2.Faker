using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Generators
{
    class UshortGenerator : Generator
    {
        public UshortGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(ushort);
        }

        public override object GenerateValue(Func<Type, object> generate)
        {
            byte[] bytes = new byte[2];
            Random.NextBytes(bytes);
            return BitConverter.ToUInt16(bytes);
        }
    }
}
