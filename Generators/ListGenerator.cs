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

        public override object GenerateValue()
        {
            if (generators.TryGetValue(nestedType, out Generator generator))
            {
                Type listType = GeneratedType.MakeGenericType(new[] { nestedType });
                IList list = (IList)Activator.CreateInstance(listType);


                int amount = Random.Next(3, 5);
                for (int i = 0; i < amount; i++)
                {
                    list.Add(generator.GenerateValue());
                }
                return list;
            }
            return null;
        }

    }
}
