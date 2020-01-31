using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IocLibrary.Attributes;

namespace IocLibrary.Container
{
    public class IocContainer
    {
        private readonly Dictionary<Type, Dictionary<string, Func<object>>> _typeDictionary = new Dictionary<Type, Dictionary<string, Func<object>>>();

        private static IocContainer _instance;
        private static readonly object LockObject = new object();

        public static IocContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new IocContainer();
                        }
                    }
                }
                return _instance;
            }
        }
        
        public void Register<TIn, TOut>(string name = "") where  TOut : TIn
        {
            var inType = typeof(TIn);
            var outType = typeof(TOut);
            Register(inType, outType, name);
            // _typeDictionary.Add(typeof(TIn), () => GetInstance(typeof(TOut)));
        }

        public void Register(Type inType, Type outType, string name = "")
        {
            if (string.IsNullOrEmpty(name))
                name = inType.Name;
            if (_typeDictionary.TryGetValue(inType, out var nameDictionary))
            {
                nameDictionary.Add(name, () => GetInstance(outType, name));
            }
            else
            {
                _typeDictionary.Add(inType, new Dictionary<string, Func<object>>
                {
                    {name, () => GetInstance(outType, name)}
                });
            }
        }

        public void RegisterSingleton(Type type)
        {
            var instance = GetInstance(type);
            _typeDictionary.Add(type, new Dictionary<string, Func<object>>
            {
                {type.Name, () => instance }
            });
        }

        public void RegisterSingleton<T>(T instance)
        {
            _typeDictionary.Add(typeof(T), new Dictionary<string, Func<object>>
            {
                { typeof(T).Name, () => instance }
            });
        }

        public T GetInstance<T>(string name = "")
        {
            return (T) GetInstance(typeof(T), name);
        }

        public List<T> GetInstances<T>()
        {
            var instances = new List<T>();
            if (_typeDictionary.TryGetValue(typeof(T), out var instantiatable))
            {
                foreach (var instantiatableKey in instantiatable.Keys)
                {
                    var instance = (T) instantiatable[instantiatableKey]();
                    instances.Add(instance);
                }
            }

            return instances;
        }

        public object GetInstance(Type type, string name = "")
        {
            if (string.IsNullOrEmpty(name))
                name = type.Name;
            if (_typeDictionary.TryGetValue(type, out var instantiatable))
            {
                if (instantiatable.TryGetValue(name, out var objectCreator))
                {
                    return objectCreator();
                }
            }
            var constructor = type.GetConstructors().OrderByDescending( o=> o.GetParameters().Length).First();
            var args = constructor.GetParameters().Select(param => GetInstance(param.ParameterType)).ToArray();
            var instance = constructor.Invoke(args);

            // now here comes the property injection
            InjectProperties(ref instance);

            // Alternative way:
            // Activator.CreateInstance(type, args);
            return instance;
        }

        private void InjectProperties(ref object item)
        {
            var properties = item.GetType().GetProperties()
                .Where(prop => prop.IsDefined(typeof(InjectAttribute), true));
            foreach (var property in properties)
            {
                var instance = GetInstance(property.PropertyType);
                property.SetValue(item,instance);
            }
        }

        public void AddAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(o => o.IsDefined(typeof(RegisterAttribute)));
            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<RegisterAttribute>();
                var inType = attribute.Type ?? type;
                Register(inType, type, attribute.Name);
            }
        }
    }
}
