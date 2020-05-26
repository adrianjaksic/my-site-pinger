using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pinger.Model
{
    public class SiteStatus
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Site")]
        public short Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime PingTime { get; set; }

        [StringLength(2000)]
        public string Status { get; set; }

        public virtual Site Site { get; set; }
    }
}
