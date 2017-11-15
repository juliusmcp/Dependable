using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependable.DataTypes
{
    public class RegisteredTypeKey
    {

        public Type From { get; private set; }
        public string NamedBinding { get; private set; }

        public RegisteredTypeKey(Type From)
        {
            this.From = From;
            this.NamedBinding = string.Empty;
        }
        public RegisteredTypeKey(Type From, string NamedBinding)
        {
            this.From = From;
            this.NamedBinding = NamedBinding;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as RegisteredTypeKey;

            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return From == other.From && string.Equals(this.NamedBinding, other.NamedBinding, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + this.From.GetHashCode();
                hash = hash * multiplier + (NamedBinding == null ? 0 : NamedBinding.GetHashCode());

                return hash;
            }
        }

    }
}
