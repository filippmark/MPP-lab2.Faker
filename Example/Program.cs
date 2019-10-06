using System;
using FakerImplementation;
using ClassesForDTO;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Faker faker = new Faker();
            faker.Create<Class1>();
            Console.WriteLine("Hello World!");
        }
    }
}
