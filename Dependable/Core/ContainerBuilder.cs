using System;
using System.Collections.Generic;
using Dependable.Store;
using Dependable.DataTypes;
using Dependable.Core;
using System.Reflection;

namespace Dependable
{
    public class ContainerBuilder 
    {
      
        private IStore _store;
        private string _lastBindingName;
        private Type _lastBindingType;
      
        public ContainerBuilder()
        {
            this._store = new Store.Store();
           
        }


        public ContainerBuilder Bind<TFrom>() where TFrom : class
        {
            this._lastBindingName = string.Empty;
            this._lastBindingType = typeof(TFrom);
            this._store.Register(this._lastBindingType, typeof(TFrom));
            return this;
        }

        public ContainerBuilder Bind<TFrom, TTo>() where TTo : TFrom
        {
            this._lastBindingName = string.Empty;
            this._lastBindingType = typeof(TFrom);
            this._store.Register(this._lastBindingType, typeof(TTo));
            return this;
        }


        public ContainerBuilder Bind<TFrom, TTo>(string BindingName) where TTo : TFrom
        {
            this._lastBindingName = BindingName;
            this._lastBindingType = typeof(TFrom);
            this._store.Register(this._lastBindingType, typeof(TTo), this._lastBindingName);
            return this;
        }
        /// <summary>
        /// Register Delegate to Type
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <param name="resolver"></param>
        public ContainerBuilder Bind<TFrom>(Func<object> Resolver)
        {
            this._lastBindingName = string.Empty;
            this._lastBindingType = typeof(TFrom);
            this._store.Register<TFrom>(Resolver);
            return this;

        }


        public ContainerBuilder Bind<TFrom>(Func<object> Resolver, string BindingName)
        {
            this._lastBindingName = BindingName;
            this._lastBindingType = typeof(TFrom);
            this._store.Register<TFrom>(Resolver, this._lastBindingName);
            return this;

        }

        public ContainerBuilder AddScope(Scope Scope)
        {
            
            this._store.UpdateScope(this._lastBindingType, Scope, this._lastBindingName);
            return this;
        }
        public ContainerBuilder AddConstructorArguments(Parameter Parameter)
        {
            this._store.AddParameter(this._lastBindingType,Parameter, this._lastBindingName);
            return this;
        }
        public ContainerBuilder AddConstructorArguments(string ParameterName, object Argument)
        {
            Parameter arg = new Parameter(ParameterName, Argument);
            this._store.AddParameter(this._lastBindingType, arg, this._lastBindingName);
            return this;
        }
    
       
        public IContainer Build()
        {
            return new Container(this._store);
        }

        public ContainerBuilder AutoRegister(Assembly Assembly)
        {

            AutoRegister autoreg = new AutoRegister(Assembly);
            Dictionary<RegisteredTypeKey, Type> mappings = autoreg.Import();
            foreach (var mapping in mappings)
            {
                this._store.Register(mapping.Key.From, mapping.Value, mapping.Key.NamedBinding);
            }
            return this;
        }
        public ContainerBuilder AutoRegister<T>(Assembly Assembly)
        {

            AutoRegister autoreg = new AutoRegister(Assembly);
            Dictionary<RegisteredTypeKey, Type> mappings = autoreg.Import<T>();
            foreach (var mapping in mappings)
            {
                this._store.Register(mapping.Key.From, mapping.Value, mapping.Key.NamedBinding);
            }
            return this;
        }
        public ContainerBuilder AutoRegister(string Dll)
        {
            AutoRegister autoreg = new AutoRegister(Dll);
            Dictionary<RegisteredTypeKey, Type> mappings = autoreg.Import();
            foreach (var mapping in mappings)
            {
                this._store.Register(mapping.Key.From, mapping.Value, mapping.Key.NamedBinding);
            }
            return this;
    
        }
        public ContainerBuilder AutoRegister<T>(string Dll)
        {
            AutoRegister autoreg = new AutoRegister(Dll);
            Dictionary<RegisteredTypeKey, Type> mappings = autoreg.Import<T>();
            foreach (var mapping in mappings)
            {
                this._store.Register(mapping.Key.From, mapping.Value, mapping.Key.NamedBinding);
            }
            return this;

        }
    }
}
