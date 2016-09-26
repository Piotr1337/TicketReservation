namespace TicketReservation.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Artists
    {
        [Key]
        public int ArtistID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(10)]
        public string ImageData { get; set; }

        public int? EventID { get; set; }

        [StringLength(10)]
        public string CategoryID { get; set; }

        [StringLength(50)]
        public string Nickname { get; set; }

        public bool? IsBand { get; set; }

        [StringLength(50)]
        public string BandName { get; set; }

        [StringLength(50)]
        public string ImageMimeType { get; set; }

        public virtual Events Events { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
