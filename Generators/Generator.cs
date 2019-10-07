using System;

namespace Generators
{
    public abstract class Generator
    {
        public Random Random { get; private set; }

        public Generator(Random randomizer)
        {
            Random = randomizer;
        }

        public abstract object GenerateValue();
    }
}
