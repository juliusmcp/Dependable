using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dependable;
using DI.TESTMODELS;
namespace DI.TESTS
{
    [TestClass]
    public class TestsA
    {
        [TestMethod]
        public void Single_Class_No_Parameters()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<ITest, Test>();
            var container=build.Build();
            var testclass=container.Resolve<ITest>();
            string result = testclass.ToUpper("test");
            Assert.AreEqual("TEST", result);

        }

        [TestMethod]
        public void Single_Class_Single_Value_Type_Parameter()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<ITest, Test>().AddConstructorArguments("Extra", "extra");
            var container = build.Build();
            var testclass = container.Resolve<ITest>();
            string result = testclass.ToUpper("test");
            Assert.AreEqual("TESTextra", result);

        }

        [TestMethod]
        public void Single_Class_Lambda_No_Parameters()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<ITest>(() => new Test());
            var container = build.Build();
            var testclass = container.Resolve<ITest>();
            string result = testclass.ToUpper("test");
            Assert.AreEqual("TEST", result);

        }
        [TestMethod]
        public void Single_Class_Lambda_Single_Value_Type_Parameter()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<ITest>(() => new Test("extra"));
            var container = build.Build();
            var testclass = container.Resolve<ITest>();
            string result = testclass.ToUpper("test");
            Assert.AreEqual("TESTextra", result);

        }
        [TestMethod]
        public void Parent_Child_Class_No_Parameters()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<ITest, Test>();
            build.Bind<IParent, Parent>();
            var container = build.Build();
            var testclass = container.Resolve<IParent>();
            string result = testclass.Run("test");
            Assert.AreEqual("TEST", result);

        }
        [TestMethod]
        public void Parent_Child_Class_Mixed_Child_Single_Value_Type_Parameter()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<ITest>(() => new Test("extra"));
            build.Bind<IParent, Parent>();
            var container = build.Build();
            var testclass = container.Resolve<IParent>();
            string result = testclass.Run("test");
            Assert.AreEqual("TESTextra", result);

        }

        [TestMethod]
        public void Parent_Child_Class_Lambda_Child_Single_Value_Type_Parameter()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<IParent>(() => new Parent(new Test("extra")));
            var container = build.Build();
            var testclass = container.Resolve<IParent>();
            string result = testclass.Run("test");
            Assert.AreEqual("TESTextra", result);

        }
        [TestMethod]
        public void Parent_Child_Class_Single_Value_Type_Parameter()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<ITest, Test>().AddConstructorArguments("Extra", "extra");
            build.Bind<IParent, Parent>();
            var container = build.Build();
            var testclass = container.Resolve<IParent>();
            string result = testclass.Run("test");
            Assert.AreEqual("TESTextra", result);

        }
        [TestMethod]
        public void Parent_No_Child_Class_Using_Lambda()
        {
            ContainerBuilder build = new ContainerBuilder();

            build.Bind<IParent, ParentAlt>();
            var container = build.Build();
            var testclass = container.Resolve<IParent>();
            string result = testclass.Run("test");
            Assert.AreEqual("sorry no upper", result);

        }
        [TestMethod]
        public void Parent_Lambda()
        {
            ContainerBuilder build = new ContainerBuilder();

            build.Bind<IParent>(() => new ParentAlt());
            var container = build.Build();
            var testclass = container.Resolve<IParent>();
            string result = testclass.Run("test");
            Assert.AreEqual("sorry no upper", result);

        }

        [TestMethod]
        public void Single_Class_No_Parameters_Self_Bind()
        {
            ContainerBuilder build = new ContainerBuilder();
            build.Bind<Test>();
            var container = build.Build();
            var testclass = container.Resolve<Test>();
            
            Assert.AreEqual(typeof(Test), testclass.GetType());
            string result = testclass.ToUpper("test");
            Assert.AreEqual("TEST", result);

        }
    }
}
