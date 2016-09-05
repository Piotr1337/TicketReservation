using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TicketReservation.Domain.Entities;
using TicketReservation.Models;

namespace TicketReservation.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<AdminViewModel, Events>();
            CreateMap<TicketViewModel, Ticket>();
            CreateMap<MemberLoginViewModel, Members>();
        }
    }
}