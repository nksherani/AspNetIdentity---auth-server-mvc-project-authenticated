using CMS.Controllers.MVCControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace CMS.Controllers
{
    //public class CustomControllerFactory : IControllerFactory
    //{
    //    public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
    //    {
    //        IUserSession session = new UserSession();
    //        Controller controller;
    //        if (controllerName == "Home")
    //            controller = new HomeController(session);
    //        else
    //            controller = new AccountController();
    //        return controller;
    //    }
    //    public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(
    //       System.Web.Routing.RequestContext requestContext, string controllerName)
    //    {
    //        return SessionStateBehavior.Default;
    //    }
    //    public void ReleaseController(IController controller)
    //    {
    //        IDisposable disposable = controller as IDisposable;
    //        if (disposable != null)
    //            disposable.Dispose();
    //    }
    //}
    public class CustomControllerFactory : IControllerFactory
    {
        private readonly string _controllerNamespace;
        public CustomControllerFactory(string controllerNamespace)
        {
            _controllerNamespace = controllerNamespace;
        }
        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            var controllername = controllerName[0].ToString().ToUpper() + controllerName.Substring(1);
            IUserSession session = new UserSession();
            string QualifiedControllerName = string.Concat(_controllerNamespace, ".", controllername, "Controller");
            Type controllerType = Type.GetType(QualifiedControllerName);
            IController controller = Activator.CreateInstance(controllerType, new[] { session }) as Controller;
            return controller;
        }
        public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(
           System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }
        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}