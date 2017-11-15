using System;
using System.Collections.Generic;
using Dependable.DataTypes;
namespace Dependable
{
    public interface IStore
    {
        void Register<TFrom, TTo>(string instanceName = "") where TTo : TFrom;
        void Register<TFrom>(Func<object> To, string instanceName = "");

        void Register(Type From, Type To, string NamedBinding = "");
        void UpdateScope(Type From, Scope Scope, string NamedBinding = "");

        void UpdateScope<TFrom>(Scope Scope, string NamedBinding = "");
        Binding FindType(Type From, string NamedBinding = "");
        void UpdateInstance(Type From, Object Instance, string NamedBinding = "");

        void UpdateInstance<TFrom>(Object Instance, string NamedBinding = "");
        IEnumerable<KeyValuePair<RegisteredTypeKey, Binding>> ListBindings();
        void AddParameter(Type From, Parameter Parameter, string NamedBinding = "");
  
    }
}
