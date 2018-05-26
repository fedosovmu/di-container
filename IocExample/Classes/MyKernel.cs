using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IocExample.Classes
{
    public class MyKernel
    {
        private readonly Dictionary<Type, Type> _types = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();


        public void Bind<TContract, TImplementation>()
        {
            _types[typeof(TContract)] = typeof(TImplementation);
        }


        public void Bind<TContract, TImplementation>(TImplementation instance)
        {
            _instances[typeof(TContract)] = instance;
        }


        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }


        public object Get(Type contract)
        {
            if (_instances.ContainsKey(contract))
            {
                return _instances[contract];
            }
            else
            {
                Type implementation = _types[contract];

                ConstructorInfo constructor = Utils.GetSingleConstructor(implementation);
                ParameterInfo[] constructorParameters = constructor.GetParameters();

                if (constructorParameters.Length == 0)
                {
                    return Activator.CreateInstance(implementation);
                }

                List<object> parameters = new List<object>(constructorParameters.Length);

                foreach (ParameterInfo parameterInfo in constructorParameters)
                {
                    parameters.Add(Get(parameterInfo.ParameterType));
                }

                return constructor.Invoke(parameters.ToArray());
            }
        }

        public object Inject<T>()
        {
            return _instances[typeof(T)];
        }

        public object ToConstructor<T>()
        {
            return (T)Utils.CreateInstance(typeof(T));
        }

        
        public object ToConstructor<T>(List<object> parameters)
        {
            return (T)Utils.CreateInstance(typeof(T), parameters);
        }


    }
}
