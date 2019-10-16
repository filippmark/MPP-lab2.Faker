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
            var dto = faker.Create<ClassWithParametrCTOR>();
            Console.WriteLine(dto.str);
            /*Class1 classik = faker.Create<Class1>();
            Console.WriteLine(classik.Listik[0] == null ? "kek" : "lol");*/
        }
    }
}
