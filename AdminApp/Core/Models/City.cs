﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Core.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
    }
}
