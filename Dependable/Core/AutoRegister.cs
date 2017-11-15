using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Dependable.DataTypes;
namespace Dependable.Core
{
    public class AutoRegister
    {

        private readonly Assembly _assem;
        public AutoRegister(Assembly Assembly)
        {
            this._assem = Assembly;
           
        }
        public AutoRegister(string AssemblyDLL)
        {
            this._assem = Assembly.LoadFrom(AssemblyDLL);
        }
        public Dictionary<RegisteredTypeKey, Type> Import()
        {
            Dictionary<RegisteredTypeKey, Type> mappings = new Dictionary<RegisteredTypeKey, Type>();
            Type[] types = this._assem.GetTypes().Where(n => n.IsClass).ToArray();
            foreach (Type type in types)
            {
                RegisteredTypeKey key = new RegisteredTypeKey(type);
                mappings.Add(key, type);
            }
            return mappings;
        }

        public Dictionary<RegisteredTypeKey, Type> Import<T>() 
        {
            Type from = typeof(T);
            Dictionary<RegisteredTypeKey, Type> mappings = new Dictionary<RegisteredTypeKey, Type>();
            var types = this._assem.GetTypes().Where(n => n.IsClass); 
            foreach (Type type in types)
            {
                //if concrete type passed bind to self
                if (type == from)
                {
                    RegisteredTypeKey key = new RegisteredTypeKey(type, type.FullName);
                    mappings.Add(key, type);
                }
                else
                {
                    //if interface passed look for concrete class that satisify interface
                    Type[] interfs = type.GetInterfaces();
                    if (interfs.Contains(from))
                    {
                        RegisteredTypeKey key = new RegisteredTypeKey(from, type.FullName);
                        mappings.Add(key, type);
                    }

                }
            }
            return mappings;
        }
    }
}
