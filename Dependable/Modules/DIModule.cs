using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dependable;
namespace Dependable
{
    public abstract class DIModule: IModule
    {
        private readonly string name;
        public DIModule(string Name="")
        {
            this.name = Name;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }

        public abstract void Load();
    }
}
