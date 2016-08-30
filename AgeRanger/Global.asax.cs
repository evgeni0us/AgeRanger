using Ninject;
using Ninject.Syntax;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AgeRanger.Data.Helpers;
using AgeRanger.Data.Repositories;
using AgeRanger.Model.Services;
using Ninject.Web.WebApi;

namespace AgeRanger
{
    


    public class MvcApplication : NinjectHttpApplication //System.Web.HttpApplication
    {



        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            //// Helpers
            kernel.Bind<IDbConnectionManager>().To<SqLiteDbConnectionManager>();


            //// Repositories
            kernel.Bind<IAgeGroupRepository>().To<SqLiteAgeGroupRepository>();
            kernel.Bind<IPersonRepository>().To<SqLitePersonRepository>();

            //// Services
            kernel.Bind<IAgeRangerService>().To<AgeRangerService>();

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            return kernel;
        }


    }
}
