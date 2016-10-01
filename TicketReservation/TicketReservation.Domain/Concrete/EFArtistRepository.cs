using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TicketReservation.Domain.Abstract;
using TicketReservation.Domain.Entities;

namespace TicketReservation.Domain.Concrete
{
    public class EFArtistRepository : IArtistRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Artists> Artists
        {
            get
            {
                return context.Artists;
            }
        }

        public void SaveArtist(Artists theArtist)
        {
            try
            {
                if (theArtist.ArtistID == 0)
                {
                    context.Artists.Add(theArtist);
                }
                else
                {
                    Artists dbEntry = context.Artists.Find(theArtist.ArtistID);
                    if (dbEntry != null)
                    {
                        var mapping = Mapper.Map(theArtist, dbEntry);
                        if (!string.IsNullOrEmpty(theArtist.ImageMimeType))
                        {
                            mapping.ImageData = theArtist.ImageData;
                            mapping.ImageMimeType = theArtist.ImageMimeType;
                        }
                    }
                }
                context.SaveChanges();
            }
            catch(DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
           
        }
    }
}
