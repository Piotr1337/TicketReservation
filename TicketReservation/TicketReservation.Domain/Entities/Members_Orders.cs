namespace TicketReservation.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Members_Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Members_Orders()
        {
            Ticket = new HashSet<Ticket>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        public int? MemberID { get; set; }

        [StringLength(10)]
        public string order_status { get; set; }

        public DateTime? order_date { get; set; }

        [StringLength(50)]
        public string order_phone_number { get; set; }

        [StringLength(50)]
        public string order_card_number { get; set; }

        public virtual Members Members { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
