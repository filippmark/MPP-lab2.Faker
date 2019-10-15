using System;
using System.Collections.Generic;
using System.Text;

namespace Generators
{
    public class DateTimeGenerator : Generator
    {
        public DateTimeGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(DateTime);
        }

        public override object GenerateValue()
        {
            byte[] buf = new byte[8];
            Random.NextBytes(buf);
            long ticks = BitConverter.ToInt64(buf, 0);
            return new DateTime(Math.Abs(ticks % DateTime.MaxValue.Ticks));
        }
    }
}
