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
            gens.Add(typeof(List<string>), null);
            Faker faker = new Faker();
            Console.WriteLine(typeof(List<string>));
        }
    }
}
