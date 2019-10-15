using ClassesForDTO;
using FakerImplementation;
using System;
using System.Collections.Generic;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Type, object> gens = new Dictionary<Type, object>();
            Faker faker = new Faker();
            Class1 classik = faker.Create<Class1>();
            Console.WriteLine(typeof(List<string>).GetGenericTypeDefinition());
            Console.WriteLine(typeof(List<string>).GenericTypeArguments[0]);
        }
    }
}
