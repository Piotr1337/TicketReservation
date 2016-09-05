namespace TicketReservation.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        public int TicketID { get; set; }

        public int EventID { get; set; }

        public DateTime DateOfEvent { get; set; }

        [Required]
        [StringLength(50)]
        public string Location { get; set; }

        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public int? OrderID { get; set; }

        public virtual Events Events { get; set; }

        public virtual Members_Orders Members_Orders { get; set; }
    }
}
