using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IocExample.Classes
{
    public static class MyKernel
    {
        private static Utils utils = new Utils();
        private static readonly Dictionary<Type, Type> types = new Dictionary<Type, Type>();
        private static readonly Dictionary<Type, object> typeInstances = new Dictionary<Type, object>();


        public static void Bind<TContract, TImplementation>()
        {
            types[typeof(TContract)] = typeof(TImplementation);
        }


        public static void Bind<TContract, TImplementation>(TImplementation instance)
        {
            typeInstances[typeof(TContract)] = instance;
        }


        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }


        public static object Resolve(Type contract)
        {
            if (typeInstances.ContainsKey(contract))
            {
                return typeInstances[contract];
            }
            else
            {
                Type implementation = types[contract];

                ConstructorInfo constructor = Utils.GetSingleConstructor(implementation);
                ParameterInfo[] constructorParameters = constructor.GetParameters();

                if (constructorParameters.Length == 0)
                {
                    return Activator.CreateInstance(implementation);
                }

                List<object> parameters = new List<object>(constructorParameters.Length);

                foreach (ParameterInfo parameterInfo in constructorParameters)
                {
                    parameters.Add(Resolve(parameterInfo.ParameterType));
                }

                return constructor.Invoke(parameters.ToArray());
            }
        }

        public static object Inject<T>()
        {
            return typeInstances[typeof(T)];
        }

        public static object Get<T>()
        {
            return (T)Utils.CreateInstance(typeof(T));
        }

        
        public static object Get<T>(List<object> parameters)
        {
            return (T)Utils.CreateInstance(typeof(T), parameters);
        }


    }
}
