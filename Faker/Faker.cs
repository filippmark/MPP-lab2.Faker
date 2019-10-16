using Generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FakerImplementation
{
    public class Faker : IFaker
    {
        private Dictionary<Type, Generator> _generators;
        private Stack<Type> _typesInProgress;
        private Random _random;

        public Faker()
        {
            _random = new Random();
            _generators = new Dictionary<Type, Generator>();
            _typesInProgress = new Stack<Type>();
            //UploadPlugins(@"C:\Users\lenovo\source\repos\MPP-lab2.Faker1\Plugins", _generators);
            AddGenerators(_generators);
        }

        public T Create<T>()
        {
            if(_typesInProgress.Contains(typeof(T)))
            {
                return default;
            }
            else
            {
                _typesInProgress.Push(typeof(T));
                object dto;
                Dictionary<string, object> constructorParams = GenerateValuesForConstructor(typeof(T), out dto);
                if (dto != null)
                {
                    GeneratePublicProperties(typeof(T), constructorParams, dto);
                    GenerateFields(typeof(T), constructorParams, dto);
                    _typesInProgress.Pop();
                    return (T)dto;
                }
                else
                {
                    _typesInProgress.Pop();
                    return default;
                }
            }
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
                var parameter = GenerateValue(param.ParameterType);
                if (parameter == null)
                {
                    dto = null;
                    return null;
                }
                generatedParams.Add(param.Name, parameter);
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

        private void GenerateFields(Type type, Dictionary<string, object> initialized, object dto)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (!initialized.ContainsKey(field.Name))
                {
                    field.SetValue(dto, GenerateValue(field.FieldType));
                }
            }
        }

        private void AddGenerators(Dictionary<Type, Generator> generators)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Generator));
            var types = assembly.DefinedTypes;
            _random = new Random();

            foreach (var type in types)
            {
                if (!(type.IsAbstract || type.IsInterface))
                {
                    Console.WriteLine(type);
                    ConstructorInfo[] constructorInfo = type.GetConstructors();
                    Generator generator = (Generator)Activator.CreateInstance(type, _random);
                    generators.Add(generator.GeneratedType, generator);
                }
            }
        }

        private void UploadPlugins(string path, Dictionary<Type, Generator> generators)
        {
            Type pluginType = typeof(Generator);
            List<Type> plugins = new List<Type>();
            var pluginDirectory = new DirectoryInfo(path);

            if (pluginDirectory.Exists)
            {

                string[] pluginFiles = Directory.GetFiles(path, "*.dll");
                foreach (var file in pluginFiles)
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    Type[] types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if ((!(type.IsInterface || type.IsAbstract)) && (type.IsSubclassOf(typeof(Generator))))
                        {
                            Console.WriteLine(type);
                            plugins.Add(type);
                        }
                    }
                }

                Random random = new Random();
                foreach (var plugin in plugins)
                {
                    Generator generator = (Generator)Activator.CreateInstance(plugin, random);
                    generators.Add(generator.GeneratedType, generator);
                }
            }
        }

        private object GenerateValue(Type type)
        {
            if ((type.IsGenericType) && (!type.GenericTypeArguments[0].IsGenericType))
            {
                if( _generators.TryGetValue(type.GetGenericTypeDefinition(), out Generator generator) )
                {
                    generator.TryToSetNestedType(type.GenericTypeArguments[0]);
                    generator.TryToSetDictWithGens(_generators);
                    return generator.GenerateValue(GenerateValue);
                }
            }
            else
            {
                if (_generators.TryGetValue(type, out Generator generator))
                {
                    return generator.GenerateValue(GenerateValue);
                }
                else if (type.IsEnum)
                {
                    var enumValues = type.GetEnumValues();
                    return enumValues.GetValue(_random.Next(enumValues.Length));
                }

                if ((!type.IsInterface) && (!type.IsAbstract))
                {
                    if ((type.IsClass) && (!Equals(type.Namespace, "System")))
                    {
                        return this.GetType().GetMethod(nameof(Create)).MakeGenericMethod(type).Invoke(this, null);
                    }
                    else if ((type.IsValueType) && (!Equals(type.Namespace, "System")))
                    {
                        Dictionary<string, object> initialized = new Dictionary<string, object>();
                        object dto = Activator.CreateInstance(type);
                        GeneratePublicProperties(type, initialized, dto);
                        GenerateFields(type, initialized, dto);
                        return dto;
                    }
                }
            }
            Console.WriteLine(type.FullName);
            return null;
        }
    }
}