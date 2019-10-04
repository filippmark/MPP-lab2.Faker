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
            GeneratePublicProperties(typeof(T), constructorParams, dto);
            return (T)dto;
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
            Dictionary<string, object> generatedParams = new Dictionary<string, object>();
            foreach (var param in parameters)
            {
                generatedParams.Add(param.Name, GenerateValue(param.ParameterType));
            }
            dto = constructor.Invoke(generatedParams.Values.ToArray());
            return generatedParams;
        }

        private void GeneratePublicProperties(Type type, Dictionary<string, object> initialized, object dto)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.CanWrite && (!initialized.ContainsKey(property.Name)))
                {
                    property.SetValue(dto, GenerateValue(property.PropertyType));
                }
            }
        }

        private void GenerateFields(Type type, object dto)
        {

        }


        private object GenerateValue(Type type)
        {
            return null;
        }
    }
}
