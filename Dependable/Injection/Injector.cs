using System;
using System.Collections.Generic;
using System.Linq;
using Dependable.DataTypes;
namespace Dependable
{
    public class Injector : IInject
    {
        private readonly IContainer _container;
        public Injector(IContainer Container)
        {
            this._container = Container;
        }


        public T GetInstance<T>() where T : class
        {
            return (T)GetInstance(typeof(T));
        }
        /// <summary>
        /// Gets an injected instance of a given type.
        /// </summary>
        /// <param name="fromType">The type to instantiate and inject.</param>
        /// <returns>Returns a new instance of the given type.</returns>
        public object GetInstance(Type fromType)
        {
            return Activator.CreateInstance(fromType, null);
        }
        /// <summary>
        /// Gets an injected instance of a given Registered type.
        /// </summary>
        /// <param name="fromType">The type to instantiate and inject.</param>
        /// <returns>Returns a new instance of the given type.</returns>
        public object GetInstance(Binding Binding)
        {
            if (Binding == null) { throw new Exception(); }
            if (Binding.IsType)
            {
                return GetConcreteInstance(Binding);

            }
            else
            {
                return Binding.Delegate();
            }



        }

      

        private object GetConcreteInstance(Binding Binding)
        {
            List<Construct> constructorlist = new List<Construct>();
            foreach (var constructor in Binding.ConcreteType.GetConstructors())
            {
                Construct construct = ExamineConstructor(constructor, Binding.Parameters);
                constructorlist.Add(construct);
                


            }
            if (constructorlist.Count > 0)
            {
                Construct constructtouse = constructorlist.Where(v=>v.Valid).OrderByDescending(n => n.Arguments.Count).FirstOrDefault();
                if (constructorlist != null)
                {
                    return Activator.CreateInstance(Binding.ConcreteType, constructtouse.GetArguments());
                }

            }
            return Activator.CreateInstance(Binding.ConcreteType, null);
        }


        private Construct ExamineConstructor(System.Reflection.ConstructorInfo Constructor, List<Parameter> BindingParameters)
        {
           
            Dictionary<string,object> args = new Dictionary<string,object>();
            var parms = Constructor.GetParameters();
            //if one or more BindingParameters have been provided then if the Constructor param count is less than this count no point in examing this constructor option
            //if no other constructor work container will try to use the default constructor
            if (parms.Length >= BindingParameters.Count)
            {
                foreach (var parm in parms)
                {
                    Parameter bindingparm = BindingParameters.FirstOrDefault(n => n.ParameterName.Equals(parm.Name));
                    if (bindingparm!=null)
                    {
                        args.Add(parm.Name,bindingparm.Argument);

                    }
                    else
                    {
                        if (parm.ParameterType.IsInterface || parm.ParameterType.IsAbstract)
                        {
                            Type parmType = parm.ParameterType;
                            object parmValue = this._container.Resolve(parmType);
                            if (parmValue != null)
                            {
                                args.Add(parm.Name, parmValue);
                            } else
                            {
                                return new Construct(Constructor.DeclaringType.ToString(), "Could not resolve " + parm.Name);

                            }
                        } else
                        {
                            return new Construct(Constructor.DeclaringType.ToString(), "Tried to infer a non interface or non abstract type. " + parm.Name);
                        }
                    }
                }
                return new Construct(Constructor.DeclaringType.ToString(), args);
            }
            return new Construct(Constructor.DeclaringType.ToString());

        }

    }
 }
