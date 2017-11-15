using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dependable;
using DI.TESTMODELS;
using Dependable.DataTypes;
using System.Linq;
namespace DI.TESTS
{
    [TestClass]
    public class TestsC
    {
        [TestMethod]
        public void Simple_CLass_Scope_Test()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<IFakeService, FakeService>().AddScope(Scope.Singleton);
            build.Bind<IFakeLogger, FakeLogger>().AddScope(Scope.NotManaged);
            var container = build.Build();
            var singletonclass = container.Resolve<IFakeService>();
            Assert.AreEqual(container.Resolve<IFakeService>(), singletonclass);
            var transientclass = container.Resolve<IFakeLogger>();
            Assert.AreNotEqual(container.Resolve<IFakeLogger>(), transientclass);
        }

        [TestMethod]
        public void Named_Instance_Test()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<IFakeService, FakeService>();
            build.Bind<IFakeLogger, FakeLogger>();
            build.Bind<IChild, Child>("lower").AddConstructorArguments("Extra", "lower").AddScope(Scope.NotManaged);
            build.Bind<IChild, ChildB>("upper").AddConstructorArguments("Extra", "UPPER").AddScope(Scope.Singleton);
            var container = build.Build();
            var Transientclass = container.Resolve<IChild>("lower");
            var Singletonclass=container.Resolve<IChild>("upper");
          
            string result = Transientclass.LogToLower("TOLOWERPLEASE");
            Assert.AreEqual("tolowerplease lower", result);
            string resultb = Singletonclass.LogToLower("toupperplease");
            Assert.AreEqual("TOUPPERPLEASE UPPER", resultb);
       
        }


        [TestMethod]
        public void Named_Instance__Scope_Test()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<IFakeService, FakeService>();
            build.Bind<IFakeLogger, FakeLogger>();
            build.Bind<IChild, Child>("lower").AddConstructorArguments("Extra", "lower").AddScope(Scope.NotManaged);
            build.Bind<IChild, ChildB>("upper").AddConstructorArguments("Extra", "UPPER").AddScope(Scope.Singleton);
            var container = build.Build();
            var Transientclass = container.Resolve<IChild>("lower");
            var Singletonclass = container.Resolve<IChild>("upper");

            string result = Transientclass.LogToLower("TOLOWERPLEASE");
            Assert.AreEqual("tolowerplease lower", result);
            string resultb = Singletonclass.LogToLower("toupperplease");
            Assert.AreEqual("TOUPPERPLEASE UPPER", resultb);
            Assert.AreNotEqual(container.Resolve<IChild>("lower"), Transientclass);
            Assert.AreEqual(container.Resolve<IChild>("upper"), Singletonclass);
        }

        [TestMethod]
        public void Simple_CLass_Scope_Dispose_Test()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<IFakeService, FakeService>().AddScope(Scope.Singleton);
            build.Bind<IFakeLogger, FakeLogger>().AddScope(Scope.Managed);
            var container = build.Build();
            var preresolve = container.ListBindings().FirstOrDefault(n => n.BindingKey.From.Equals(typeof(IFakeLogger)));
            Assert.IsNotNull(preresolve);
            Assert.IsNull(preresolve.Instance);
            var singletonclass = container.Resolve<IFakeService>();
            Assert.AreEqual(container.Resolve<IFakeService>(), singletonclass);
            var transientclass = container.Resolve<IFakeLogger>();
            Assert.AreNotEqual(container.Resolve<IFakeLogger>(), transientclass);
            var postresolve = container.ListBindings().FirstOrDefault(n => n.BindingKey.From.Equals(typeof(IFakeLogger)));
            Assert.IsNotNull(postresolve);
            Assert.IsNotNull(postresolve.Instance);
            container.Dispose<IFakeLogger>();
            var cleanup = container.ListBindings().FirstOrDefault(n => n.BindingKey.From.Equals(typeof(IFakeLogger))); ;
            Assert.IsNotNull(cleanup);
            Assert.IsNull(cleanup.Instance);
        }
    }
}
