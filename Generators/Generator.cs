using System;
using System.Collections.Generic;

namespace Generators
{
    public abstract class Generator
    {
        protected Random Random;
        public Type GeneratedType { get; protected set; }

        protected Type nestedType;

        protected Dictionary<Type, Generator> generators;
        public Generator(Random randomizer)
        {
            Random = randomizer;
        }

        public bool TryToSetNestedType(Type type)
        {
            if(GeneratedType.IsGenericType)
            {
                nestedType = type;
                return true;
            }
            return false;
        }

        public bool TryToSetDictWithGens(Dictionary<Type, Generator> simpleGens)
        {
            if (GeneratedType.IsGenericType)
            {
                generators = simpleGens;
                return true;
            }
            return false;
        }

        public abstract object GenerateValue();
    }
}
