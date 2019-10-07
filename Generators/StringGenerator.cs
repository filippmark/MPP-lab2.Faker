using System;
using System.Linq;

namespace Generators
{
    class StringGenerator : Generator
    {
        public StringGenerator(Random random) : base(random)
        {

        }

        public override object GenerateValue()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 25)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}