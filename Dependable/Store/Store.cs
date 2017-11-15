using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dependable;
using Dependable.DataTypes;
namespace Dependable.Store
{
    public class Store : IStore
    {
        private Dictionary<RegisteredTypeKey, Binding> _bindings;
        
        //add locks
        public Store()
        {
            _bindings = new Dictionary<RegisteredTypeKey, Binding>();
        }



        public void Register<TFrom, TTo>(string instanceName = "") where TTo : TFrom
        {
            Register(typeof(TFrom), typeof(TTo), instanceName);
        }


        public void Register<TFrom>(Func<object> To, string instanceName = "")
        {
            if (To != null)
            {
                Register(typeof(TFrom), To as Func<object>, instanceName);

            }

        }

        public void Register(Type From, Type To, string NamedBinding="")
        {
            if (From != null && To != null)
            {
                RegisteredTypeKey key = new RegisteredTypeKey(From, NamedBinding);
                Binding registeredType = new Binding(key, To);
                addBinding(key, registeredType);
            }


        }
        public void Register(Type From, Func<object> To, string NamedBinding="")
        {
            if (From != null && To!=null)
            {
                RegisteredTypeKey key = new RegisteredTypeKey(From, NamedBinding);
                Func<object> createInstance = To as Func<object>;
                Binding registeredType = new Binding(key, To);
                addBinding(key, registeredType);
            }
        }

        public void UpdateScope<TFrom>(Scope Scope, string NamedBinding = "")
        {
            Type From = typeof(TFrom);
            RegisteredTypeKey key = new RegisteredTypeKey(From, NamedBinding);
            updateScope(key, Scope);
            

        }
        public void UpdateScope(Type From, Scope Scope, string NamedBinding="")
        {
            if (From != null)
            {
                RegisteredTypeKey key = new RegisteredTypeKey(From, NamedBinding);
                updateScope(key, Scope);
            }

        }

        public void UpdateInstance(Type From, Object Instance, string NamedBinding = "")
        {
            if (From != null)
            {
                RegisteredTypeKey key = new RegisteredTypeKey(From, NamedBinding);
                updateInstance(key, Instance);
            }

        }
        public void UpdateInstance<TFrom>(Object Instance,string NamedBinding = "")
        {
            Type From = typeof(TFrom);
            RegisteredTypeKey key = new RegisteredTypeKey(From, NamedBinding);
            updateInstance(key, Instance);
            

        }
        public void AddParameter(Type From, Parameter Parameter, string NamedBinding = "")
        {
            if (From != null && Parameter!=null)
            {
                RegisteredTypeKey key = new RegisteredTypeKey(From, NamedBinding);
                updateParameters(key, Parameter);
            }

        }
        

        private void updateMapping(RegisteredTypeKey key, Type MapTo, Dictionary<string, string> Mappings)
        {
            Binding binding;
            if (_bindings.TryGetValue(key, out binding))
            {

                if (binding.IsType)
                {
                    _bindings[key] = new Binding(binding.BindingKey, binding.ConcreteType, Scope, binding.Parameters, binding.Instance);
                }
                else
                {
                    _bindings[key] = new Binding(binding.BindingKey, binding.Delegate, Scope, binding.Instance);
                }
            }
        }
        private void addBinding(RegisteredTypeKey key, Binding registeredType)
        {
            if (key!=null && !_bindings.ContainsKey(key))
            {
                _bindings.Add(key, registeredType);
            }
        }
        private void updateScope(RegisteredTypeKey key, Scope Scope)
        {
            Binding binding;
            if (_bindings.TryGetValue(key, out binding))
            {
              
                if (binding.IsType)
                {
                    _bindings[key] = new Binding(binding.BindingKey, binding.ConcreteType, Scope, binding.Parameters, binding.Instance);
                }
                else
                {
                    _bindings[key] = new Binding(binding.BindingKey, binding.Delegate, Scope,binding.Instance);
                }
            }

        }

        private void updateParameters(RegisteredTypeKey key, Parameter Parameter)
        {
            Binding binding;
            if (_bindings.TryGetValue(key, out binding))
            {

                if (binding.IsType)
                {
                    List<Parameter> parameters = _bindings[key].Parameters;
                    if (parameters!=null)
                    {
                        parameters = new List<Parameter>(parameters);
                        parameters.Add(Parameter);
                        _bindings[key] = new Binding(binding.BindingKey, binding.ConcreteType, parameters);
                    }
                    
                }
                
            }
        }

        private void updateInstance(RegisteredTypeKey key,Object Instance)
        {
            Binding binding;
            if (_bindings.TryGetValue(key, out binding))
            {
                 if (binding.IsType)
                {
                    Binding newbinding = new Binding(key, binding.ConcreteType, binding.Scope, binding.Parameters, Instance);
                    _bindings[key] = newbinding;
                } else
                {
                    Binding newbinding = new Binding(key, binding.Delegate, binding.Scope, Instance);
                    _bindings[key] = newbinding;
                }
               
            }
        }
        public IEnumerable<KeyValuePair<RegisteredTypeKey, Binding>> ListBindings()
        {
            return _bindings.AsEnumerable<KeyValuePair<RegisteredTypeKey,Binding>>();

        }
        public Binding FindType(Type From, string NamedBinding="")
        {
            Binding registeredType = null;
            if (From != null)
            {
                RegisteredTypeKey key = new RegisteredTypeKey(From, NamedBinding);
                if (this._bindings.ContainsKey(key))
                {
                    registeredType = this._bindings.FirstOrDefault(comp => comp.Key.Equals(key)).Value;
                } else
                {
                    key = _bindings.Keys.Where(n => n.From.Equals(From)).FirstOrDefault();
                    if (key != null)
                    {
                        registeredType = this._bindings.FirstOrDefault(comp => comp.Key.Equals(key)).Value;
                    } else
                    {
                        registeredType =_bindings.Values.Where(n => n.ConcreteType.Equals(From)).FirstOrDefault();
                    }
                }
            }
            return registeredType;
        }
       
    }
}
