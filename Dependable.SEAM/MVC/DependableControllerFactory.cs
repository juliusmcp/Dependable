//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;
//using Dependable;
//using System.Web.Routing;
//namespace Dependable.SEAM.MVC
//{
//    class DependableControllerFactory : DefaultControllerFactory
//    {
//        private readonly IContainer container;

//        public DependableControllerFactory(IContainer container)
//        {
//            this.container = container;
//        }
//        protected override IController GetControllerInstance(RequestContext Context, Type controllerType)
//        {
//            return container.Resolve(controllerType) as Controller;
//        }
//    }
//}
