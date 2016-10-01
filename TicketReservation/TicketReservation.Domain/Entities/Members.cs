namespace TicketReservation.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Members
    {
        [Key]
        public int MemberID { get; set; }

        public int? address_id { get; set; }

        [StringLength(20)]
        public string first_name { get; set; }

        [StringLength(20)]
        public string last_name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string telephone { get; set; }

        [StringLength(50)]
        public string Password { get; set; }
    }
}
