using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Faker
{
    class Faker : IFaker
    {
        public T Create<T>()
        {
            object dto;
            Dictionary<string, object> constructorParams = GenerateValuesForConstructor(typeof(T), out dto);
            return;
        }

        private Dictionary<string, object> GenerateValuesForConstructor(Type type, out object dto)
        {
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            ConstructorInfo constructor = constructorInfos[0];
            foreach (var constructorVariant in constructorInfos)
            {
                if (constructorVariant.GetParameters().Length > constructor.GetParameters().Length)
                {
                    constructor = constructorVariant;
                }
            }
            ParameterInfo[] parameters = constructor.GetParameters();
            Dictionary<string, Object> generatedParams = new Dictionary<string, object>();
            foreach (var param in parameters)
            {
                generatedParams.Add(param.Name, GenerateValue(param.ParameterType));
            }
            dto = constructor.Invoke(generatedParams.Values.ToArray());
            return generatedParams;
        }

        private object GenerateValue(Type type)
        {
            return null;
        }
    }
}
