using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dependable;
using DI.TESTMODELS;
namespace DI.TESTS
{
    /// <summary>
    /// Summary description for TestsB
    /// </summary>
    [TestClass]
    public class TestsB
    {
      

        [TestMethod]
        public void Reference_And_Value_Type_Mixed_Single_Class()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<IFakeService, FakeService>();
            build.Bind<IFakeLogger, FakeLogger>();
            build.Bind<IChild, Child>();
            var container = build.Build();
            var testclass = container.Resolve<IChild>();
            string result = testclass.LogToLower("TOLOWERPLEASE");
            Assert.AreEqual("tolowerplease", result);
        }


        [TestMethod]
        public void Reference_And_Value_Type_Mixed_Single_Class_Extra_Argument()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<IFakeService, FakeService>();
            build.Bind<IFakeLogger, FakeLogger>();
            build.Bind<IChild, Child>().AddConstructorArguments("Extra","UPPER");
            var container = build.Build();
            var testclass = container.Resolve<IChild>();
            string result = testclass.LogToLower("TOLOWERPLEASE");
            Assert.AreEqual("tolowerplease UPPER", result);
        }

        [TestMethod]
        public void Reference_And_Value_Type_Mixed_Single_Class_One_Refence_Concrete_Type_Supplied()
        {
            ContainerBuilder build = new ContainerBuilder();
          
            FakeService service = new FakeService();
            build.Bind<IFakeLogger, FakeLogger>();

            build.Bind<IChild, Child>().AddConstructorArguments("Service", service);
            var container = build.Build();
            var testclass = container.Resolve<IChild>();
            string result = testclass.LogToLower("TOLOWERPLEASE");
            Assert.AreEqual("tolowerplease", result);
        }


        [TestMethod]
        public void Incomplete_Bindings_Use_PArameterless_Constructor()
        {
            ContainerBuilder build = new ContainerBuilder();

            FakeService service = new FakeService();
            build.Bind<IParentO, ParentObject>();
            var container = build.Build();
            var testclass = container.Resolve<IParentO>();
            string result = testclass.Reverse("NOWAY");
            Assert.AreEqual("YAWON", result);
        }
        [TestMethod]
        public void Incomplete_Bindings_()
        {
            ContainerBuilder build = new ContainerBuilder();

            FakeService service = new FakeService();
            build.Bind<IParentO, ParentObject>();
            build.Bind<IReverser, Reverser>();
            var container = build.Build();

            var testclass = container.Resolve<IParentO>();
            string result = testclass.Reverse("NOWAY");
            Assert.AreEqual("YAWON", result);
        }
    }
}
