using Autofac;
using Backend.Services;
using log4net;
using System.Linq;
using System.Reflection;

namespace Backend
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //This is your app entry point
        static void Main(string[] args)
        {
            var container = ConfigureContainer();

            //Get your application menu class
            var application = container.Resolve<IApplicationService>();

            //Run your application
            application.Run();
        }

        //You should configure DI container (Autofac) or other DI Framework
        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //Main
            builder.RegisterType<Backend.Services.ApplicationService>().As<Backend.Services.IApplicationService>();

            //DataAccessLayer
            builder.RegisterType<Backend.DataAccessLayer.Persons>().As<Backend.DataAccessLayer.IPersons>()
                .WithParameter("log", log);

            //Bussiness Layer
            builder.RegisterType<Backend.BussinessLogic.Persons>().As<Backend.BussinessLogic.IPersons>();
            builder.RegisterType<Backend.BussinessLogic.Salaries>().As<Backend.BussinessLogic.ISalaries>();

            return builder.Build();
        }
    }
}
