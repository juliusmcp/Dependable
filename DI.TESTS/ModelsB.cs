using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.TESTMODELS
{
    public interface IFakeService
    {
        string ToLower(string input);
        string ToUpper(string input);
    }

    public interface IFakeLogger
    {
        void Log(string input);
        
    }
    public interface IChild
    {
        string LogToLower(string input);
        

    }
    public interface IParentO
    {
        string Reverse(string input);


    }

    public interface IReverser
    {

        string ReverseString(string input);
    }
    public class FakeService : IFakeService
    {
        public string ToLower(string input)
        {
            return input.ToLower();
        }

        public string ToUpper(string input)
        {
            return input.ToUpper();
        }
    }

    public class FakeLogger : IFakeLogger
    {
        public void Log(string input)
        {
            Console.WriteLine(input);
        }
    }

    
    public class Child : IChild
    {
        private readonly IFakeLogger _logger;
        private readonly IFakeService _service;
        private readonly string extra;
        public Child (IFakeLogger Logger, IFakeService Service)
        {
            this._logger = Logger;
            this._service = Service;
        }

        public Child(IFakeLogger Logger, IFakeService Service, string Extra)
        {
            this._logger = Logger;
            this._service = Service;
            this.extra = Extra;
        }
        public string LogToLower(string input)
        {
            _logger.Log(input);
            if (extra!=null)
            {
                return this._service.ToLower(input) + " " + this.extra;
            } else
            {
                return this._service.ToLower(input);
            }
        }

       
    }
    public class ChildB : IChild
    {
        private readonly IFakeLogger _logger;
        private readonly IFakeService _service;
        private readonly string extra;
        public ChildB(IFakeLogger Logger, IFakeService Service)
        {
            this._logger = Logger;
            this._service = Service;
        }

        public ChildB(IFakeLogger Logger, IFakeService Service, string Extra)
        {
            this._logger = Logger;
            this._service = Service;
            this.extra = Extra;
        }
        public string LogToLower(string input)
        {
            _logger.Log(input);
            if (extra != null)
            {
                return this._service.ToUpper(input) + " " + this.extra;
            }
            else
            {
                return this._service.ToUpper(input);
            }
        }


    }
    public class ParentObject : IParentO
    {
        private readonly IChild _child;
        private readonly IReverser _reverse;
        public ParentObject()
        {
            this._reverse = new Reverser();
        }

        public ParentObject(IChild Child, IReverser Reverser)
        {
            this._child = Child;
            this._reverse = Reverser;
        }

        public string Reverse(string input)
        {
            if (this._child!=null)
            {
                return this._reverse.ReverseString(this._child.LogToLower(input));
            } else
            {
                return this._reverse.ReverseString(input);
            }
        }

       


    }

    public class Reverser : IReverser
    {
        public string ReverseString(string input)
        {
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
