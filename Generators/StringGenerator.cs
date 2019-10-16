using System;
using System.Linq;
using System.Reflection;

namespace Generators
{
    class StringGenerator : Generator
    {
        public StringGenerator(Random random) : base(random)
        {
            GeneratedType = typeof(string);
        }

        public override object GenerateValue(Func<Type, object> generate)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 25)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}