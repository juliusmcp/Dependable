using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.TESTMODELS
{
    public interface IParent
    {
        string Run(string Input);
    }
    public interface ITest
    {
        string ToUpper(string Input);

    }
    public class Test : ITest
    {
        private readonly string extra;

        public Test()
        {
            this.extra = string.Empty;
        }

        public Test(string Extra)
        {
            this.extra = Extra;
        }
        public string ToUpper(string Input)
        {
            return Input.ToUpper() + this.extra;
        }
    }

    public class Parent : IParent
    {
        private readonly ITest _test;

        
        public Parent(ITest Test)
        {
            this._test = Test;
        }

        public string Run(string Input)
        {
            return this._test.ToUpper(Input);

        }
    }


    public class ParentAlt : IParent
    {
        private readonly ITest _test;

        public ParentAlt()
        {

        }
        public ParentAlt(ITest Test)
        {
            this._test = Test;
        }

        public string Run(string Input)
        {
            if (_test==null)
            {
                return "sorry no upper";
            } else
            {
                return this._test.ToUpper(Input);
            }
           

        }
    }
}
