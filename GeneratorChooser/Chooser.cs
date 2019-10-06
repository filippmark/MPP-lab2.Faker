using System;
using Generators;

namespace GeneratorChooser
{
    public class Chooser
    {
        
        public object GenerateValue(Type type)
        {
            Console.WriteLine(type);
            IntGenerator intGenerator = new IntGenerator();
            return intGenerator.GenerateValue();
        }
    }
}