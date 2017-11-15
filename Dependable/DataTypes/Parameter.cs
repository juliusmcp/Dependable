using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependable.DataTypes
{
    public class Parameter
    {
        public string ParameterName { get; private set; }

        public object Argument { get; private set; }

        public Parameter(string ParameterName, object Argument)
        {
            this.ParameterName = ParameterName;
            this.Argument = Argument;
        }
    }
}
