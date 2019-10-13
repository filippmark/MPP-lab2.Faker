using GeneratorChooser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Generators;
using System.IO;

namespace FakerImplementation
{
    public class Faker : IFaker
    {
        private readonly Dictionary<string, Generator> _generator;
        private Chooser _chooser;

        public Faker()
        {
            _generator = new Dictionary<string, Generator>();
            UploadPlugins(@"/Plugins").ToList().ForEach(x => _generator[x.Key] = x.Value);
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

        private Dictionary<string, Generator> UploadPlugins(string path)
        {
            Type pluginType = typeof(Generator);
            List<Type> plugins = new List<Type>();
            Dictionary<string, Generator> generators = new Dictionary<string, Generator>();
            var pluginDirectory = new DirectoryInfo(path);

            if (pluginDirectory.Exists)
            {
                string[] pluginFiles = Directory.GetFiles(path, "*.dll");
                foreach(var file in pluginFiles)
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    Type[] types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        if ((!(type.IsInterface || type.IsAbstract)) && (type.GetInterface(pluginType.FullName) != null))
                        {
                            plugins.Add(type);
                        }
                    }
                }

                Random random = new Random();
                foreach(var plugin in plugins)
                {
                    Generator generator = (Generator)Activator.CreateInstance(plugin, random);
                    generators.Add(plugin.FullName, generator);
                }
            }

            return generators;
        }

        private object GenerateValue(Type type)
        {
            Console.WriteLine(_chooser.GenerateValue(type));
            return null;  //_chooser.GenerateValue(type);
        }
    }
}
