using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependable.DataTypes
{
    public class Binding
    {
        public RegisteredTypeKey BindingKey { get; private set; }
        public Type ConcreteType { get; private set; }
        public Func<object> Delegate { get; private set; }
        public bool IsType { get; private set; }
        public Scope Scope{ get; private set; }
        public List<Parameter> Parameters { get; private set; }

        public object Instance { get; private set; }


        public Binding(RegisteredTypeKey BindingKey, Type To)
        {
            this.BindingKey = BindingKey;
            this.ConcreteType = To;
            IsType = true;
            this.Scope = Scope.NotManaged;
            this.Parameters = new List<Parameter>();
            this.Instance = null;
        }
        public Binding(RegisteredTypeKey BindingKey, Type To, List<Parameter> Parameters)
        {
            this.BindingKey = BindingKey;
            this.ConcreteType = To;
            IsType = true;
            this.Scope = Scope.NotManaged;
            this.Parameters = Parameters;
            this.Instance = null;
        }

        public Binding(RegisteredTypeKey BindingKey, Func<object> To)
        {
            this.BindingKey = BindingKey;
            this.Delegate = To;
            IsType = false;
            this.Scope = Scope.NotManaged;
            this.Instance = null;
        }

        public Binding(RegisteredTypeKey BindingKey, Type To, Scope Scope, List<Parameter> Parameters, object Instance = null)
        {
            this.BindingKey = BindingKey;
            this.ConcreteType = To;
            this.IsType = true;
            this.Scope = Scope;
            this.Parameters = Parameters;
            this.Instance = Instance;

        }

        public Binding(RegisteredTypeKey BindingKey, Type To,  Scope Scope, object Instance=null)
        {
            this.BindingKey = BindingKey;
            this.ConcreteType = To;
            this.IsType = true;
            this.Scope = Scope;
            this.Parameters = new List<Parameter>();
            this.Instance = Instance;
        }
        public Binding(RegisteredTypeKey BindingKey, Func<object> To, Scope Scope, object Instance = null)
        {
            this.BindingKey = BindingKey;
            this.Delegate = To;
            IsType = false;
            this.Scope = Scope;
            this.Instance = Instance;
        }

       
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as Binding;

            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return BindingKey == other.BindingKey;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + this.BindingKey.GetHashCode();


                return hash;
            }
        }

    }
}
