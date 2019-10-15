using GeneratorChooser;
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
        private Chooser _chooser;

        public Faker()
        {
            _generators = new Dictionary<Type, Generator>();
            AddGenerators(_generators);
            //UploadPlugins(@"Plugins", _generator);
            _chooser = new Chooser(new Random());
        }

        public T Create<T>()
        {
            object dto;
            Dictionary<string, object> constructorParams = GenerateValuesForConstructor(typeof(T), out dto);
            GeneratePublicProperties(typeof(T), constructorParams, dto);
            GenerateFields(typeof(T), constructorParams, dto);
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
                    Console.WriteLine(property.Name);
                    Console.WriteLine(property.PropertyType);
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
            Random random = new Random();

            foreach (var type in types)
            {
                if (!(type.IsAbstract || type.IsInterface))
                {
                    Console.WriteLine(type);
                    ConstructorInfo[] constructorInfo = type.GetConstructors();
                    Generator generator = (Generator)Activator.CreateInstance(type, random);
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
                        if ((!(type.IsInterface || type.IsAbstract)) && (type.GetInterfaces().Contains(typeof(Generator))))
                        {
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
                    Console.WriteLine(generator.TryToSetNestedType(type.GenericTypeArguments[0]));
                    Console.WriteLine(generator.TryToSetDictWithGens(_generators));
                    return generator.GenerateValue();
                }
            }
            else
            {
                if(_generators.TryGetValue(type, out Generator generator))
                    return generator.GenerateValue();
            }

            return null;
        }
    }
}