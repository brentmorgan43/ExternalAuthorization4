using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExternalAuthData
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string UserId { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(500)]
        public string GivenName { get; set; }
        [StringLength(500)]
        public string SurName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        public ICollection<Claim> Claims { get; set; }

    }
}
