using System;
using System.Reflection;

namespace Generators
{
    class ByteGenerator : Generator
    {
        public ByteGenerator(Random random) : base(random)
        {
            GeneratedType = typeof(byte);
        }

        public override object GenerateValue(Func<Type, object> generate)
        {
            return (byte)Random.Next();
        }
    }
}
