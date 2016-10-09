using System.Web.Mvc;
using Newtonsoft.Json;

namespace TicketReservation.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Events
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Events()
        {
            Ticket = new HashSet<Ticket>();
        }

        [Key]
        public int EventID { get; set; }

        public int? EventCategoryID { get; set; }

        public int? PlaceID { get; set; }

        [StringLength(100)]
        public string EventName { get; set; }

        public DateTime? EventStartDateTime { get; set; }

        public DateTime? EventEndDateTime { get; set; }

        [AllowHtml]
        [JsonIgnore]
        public string OtherDetails { get; set; }

        public int? EventSubCategoryID { get; set; }

        [JsonIgnore]
        public byte[] ImageData { get; set; }

        [StringLength(50)]
        [JsonIgnore]
        public string ImageMimeType { get; set; }

        [JsonIgnore]
        public virtual Categories Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Ticket> Ticket { get; set; }

        [JsonIgnore]
        public virtual SubCategories SubCategories { get; set; }
    }
}
