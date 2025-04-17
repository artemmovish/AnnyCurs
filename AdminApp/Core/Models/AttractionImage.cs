using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Core.Models
{
    public class AttractionImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public int AttractionId { get; set; } // Явный внешний ключ

        [Required]
        [ForeignKey("AttractionId")]
        public virtual Attraction Attraction { get; set; }
    }
}
