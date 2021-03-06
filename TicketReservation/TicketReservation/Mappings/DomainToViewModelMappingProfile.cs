﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TicketReservation.Domain.Entities;
using TicketReservation.Models;

namespace TicketReservation.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<Events, AdminViewModel>();
            CreateMap<Ticket, TicketViewModel>();
            CreateMap<Members, MemberLoginViewModel>();
            CreateMap<Artists, ArtistViewModel>();
            CreateMap<Artists, Artists>();
        }
    }
}