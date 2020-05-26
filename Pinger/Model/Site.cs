using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pinger.Model
{
    public class Site
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Url { get; set; }

        public bool Active { get; set; }

        public DateTime? FirstPingTime { get; set; }

        [StringLength(2000)]
        public string Status { get; set; }

        public short SameStatus { get; set; }

        public short ErrorSent { get; set; }

        public string Emails { get; set; }

        public virtual ICollection<SiteStatus> SiteStatus { get; set; }
    }
}
