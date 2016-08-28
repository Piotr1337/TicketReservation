using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TicketReservation.Controllers;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;

namespace TicketReservation.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Events()
        {
            Mock<IEventRepository> mock = new Mock<IEventRepository>();
            mock.Setup(m => m.Events).Returns(new Events[]
            {
                new Events {EventID = 1, EventName = "E1"},
                new Events {EventID = 2, EventName = "E2"},
                new Events {EventID = 3, EventName = "E3"},
                new Events {EventID = 4, EventName = "E4"},
                new Events {EventID = 4, EventName = "E5"},
            });
        }
    }
}
