using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExternalAuthData
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(1000)]
        public string ClaimType { get; set; }
        [StringLength(1000)]
        public string ClaimValue { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
