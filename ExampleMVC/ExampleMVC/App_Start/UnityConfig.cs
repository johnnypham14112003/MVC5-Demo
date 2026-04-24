using Repository.Interfaces;
using Repository.Repository;
using Service.Interfaces;
using Service.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ExampleMVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<IMotorRepository, MotorRepository>();
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IMotorService, MotorService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}