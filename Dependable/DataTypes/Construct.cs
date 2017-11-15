
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependable.DataTypes
{
    public class Construct
    {
        public Dictionary<string, object> Arguments { get; private set; }
        public bool Valid { get; private set; }
        public string Name { get; private set; }

        public string Error { get; private set; }
        public Construct(string Name)
        {
            this.Valid = false;
            this.Name = Name;

        }
        public Construct(string Name,string Error)
        {
            this.Valid = false;
            this.Name = Name;
            this.Error = Error;
        }
        public Construct(string Name,Dictionary<string, object> Arguments)
        {
            this.Valid = true;
            this.Arguments = Arguments;
            this.Name = Name;
        }

        public Object[] GetArguments()
        {
            return this.Arguments.Select(n => n.Value).ToArray();
        }

        public IEnumerable<KeyValuePair<string,object>> GetParameterList()
        {
           return this.Arguments;
            
        }
    }
}
