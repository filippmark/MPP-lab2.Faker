using System;

namespace Generators
{
    public abstract class Generator
    {
        public Random Random { get;  }
        public Type GeneratedType { get; protected set; }

        public Generator(Random randomizer)
        {
            Random = randomizer;
        }

        public abstract object GenerateValue();
    }
}
