using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dependable.DataTypes;
namespace Dependable
{
    //refactor to dispose binding type
    public class Container : IContainer
    {
        private readonly IStore _store;
        private readonly IInject _injector;
      
        public Container(IStore Store)
        {
            this._store = Store;
            this._injector = new Injector(this);
        }

        public IEnumerable<Binding> ListBindings()
        {

            return _store.ListBindings().Select(n => n.Value);

        }
        public object Resolve(Type From, string BindingName = "")
        {
            //Target Type
            Binding target = this._store.FindType(From);
            if (target != null)
            {
                if (target.Instance != null)
                {
                    //Singleton instance found return this
                    if (target.Scope == Scope.Singleton)
                    {
                        return target.Instance;
                    }
                    else
                    {
                        Dispose(From);
                        object instance = _injector.GetInstance(target);
                        if (target.Scope == Scope.Managed)
                        {
                            this._store.UpdateInstance(target.BindingKey.From, instance, target.BindingKey.NamedBinding);
                        }
                        return instance;

                    }
                    
                }
                else
                {
                    if (target.Scope == Scope.Singleton || target.Scope == Scope.Managed)
                    {

                        object instance = _injector.GetInstance(target);
                        this._store.UpdateInstance(target.BindingKey.From, instance, target.BindingKey.NamedBinding);
                        return instance;


                    }
                    return _injector.GetInstance(target);
                }

            }
            else
            {
                return null;
            }


        }
        public T Resolve<T>(string BindingName="")
        {
            return (T)Resolve(typeof(T), BindingName);
        }

        public void Dispose(Type From)
        {
            Binding target = this._store.FindType(From);
            if (target != null)
            {
                if (target.Instance != null && target.Scope == Scope.Managed)
                {

                     IDisposable disposable = target.Instance as IDisposable;
                     if (disposable!=null)
                     {
                        disposable.Dispose();
                     }
                     
                     this._store.UpdateInstance(target.BindingKey.From, null, target.BindingKey.NamedBinding);
                }
            }

        }

        public void Dispose(Type From, string BindingName="")
        {
            Binding target = this._store.FindType(From,BindingName);
            if (target != null)
            {
                if (target.Instance != null && target.Scope == Scope.Managed)
                {

                    IDisposable disposable = target.Instance as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }

                    this._store.UpdateInstance(target.BindingKey.From, null, target.BindingKey.NamedBinding);
                }
            }

        }
        public void Dispose<TFrom>(string BindingName="")
        {
            Dispose(typeof(TFrom),BindingName);
        }
    }
}
