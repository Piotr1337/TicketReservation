using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;

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
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new List<Event>
            {
                new Event { EventName = "OrangeFestiwal", OtherDetails = "elo impreza w chuj"},
                new Event { EventName = "HipHopNaZywca", OtherDetails = "pozdrawiam"},
                new Event { EventName = "COma", OtherDetails = "no witam"},
            });

            kernel.Bind<IEventRepository>().ToConstant(mock.Object);
        }
    }
}