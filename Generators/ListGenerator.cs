using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Generators
{
    class ListGenerator : Generator
    {

        public ListGenerator(Random randomizer) : base(randomizer)
        {
            GeneratedType = typeof(List<>);
        }

        public override object GenerateValue(Func<Type, object> generate)
        {
            Type listType = GeneratedType.MakeGenericType(new[] { nestedType });
            IList list = (IList)Activator.CreateInstance(listType);
            int amount = Random.Next(3, 5);

            if (generators.TryGetValue(nestedType, out Generator generator))
            {
                for (int i = 0; i < amount; i++)
                {
                    list.Add(generator.GenerateValue(generate));
                }
                return list;
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    list.Add(generate(nestedType));
                }
                return list;
            }
        }

    }
}
