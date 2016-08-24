using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Concrete;
using TicketReservation.Domain.Entities;
using TicketReservation.Infrastructure.Abstract;
using TicketReservation.Infrastructure.Concrete;

namespace TicketReservation.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IEventRepository>().To<EFEventRepository>();
            kernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            kernel.Bind<ITicketRepository>().To<EFTicketRepository>();
        }
    }
}