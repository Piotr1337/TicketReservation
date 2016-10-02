using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Abstract
{
    public interface IArtistRepository
    {
        IEnumerable<Artists> Artists { get; }

        IEnumerable<SelectListItem> ArtistsForDropList { get; }

        void SaveArtist(Artists theArtist);

        Artists GetArtists(int artistId);

        Artists DeleteArtist(int artistId);


    }
}
