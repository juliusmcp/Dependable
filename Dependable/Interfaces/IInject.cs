using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI.Container;
using Dependable.DataTypes;
namespace Dependable
{
    public interface IInject
    {

        T GetInstance<T>() where T : class;
        object GetInstance(Type fromType);

        object GetInstance(Binding Binding);
        // IContainer ServiceResolver { get; set; }
    }
}
