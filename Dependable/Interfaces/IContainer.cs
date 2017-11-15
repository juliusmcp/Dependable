using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dependable.DataTypes;

namespace Dependable
{
    public interface IContainer
    {
       

        T Resolve<T>(string BindingName="");
        object Resolve(Type From, string BindingName="");
        void Dispose(Type From, string BindingName="");
        void Dispose<TFrom>(string BindingName="");
        IEnumerable<Binding> ListBindings();
    }
}
