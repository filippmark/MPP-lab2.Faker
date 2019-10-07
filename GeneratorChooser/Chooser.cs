using Generators;
using System;

namespace GeneratorChooser
{
    public class Chooser
    {
        private Random random;

        public Chooser(Random randomizer)
        {
            random = randomizer;
        }

        public object GenerateValue(Type type)
        {
            Console.WriteLine();
            IntGenerator intGenerator = new IntGenerator(random);
            return intGenerator.GenerateValue();
        }
    }
}