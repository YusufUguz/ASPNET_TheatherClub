using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheaterClubProject.Models
{
    [Keyless]
    public partial class Loginc
    {
        [StringLength(100)]
        [Unicode(false)]
        public string? Email { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Password { get; set; }
    }
}
