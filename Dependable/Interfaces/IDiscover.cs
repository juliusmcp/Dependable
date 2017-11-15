using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dependable.DataTypes;
namespace Dependable
{
    public interface IDiscover
    {
        IEnumerable<Binding> ListBindings();
    }
}
