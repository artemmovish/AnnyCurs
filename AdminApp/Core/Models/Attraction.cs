using AdminApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AdminApp.Core.Models
{
    public class Attraction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string MapLink { get; set; }

        [Required]
        public string Description { get; set; }

        // Список картинок (храним пути к файлам или URL)
        public virtual ICollection<AttractionImage> Images { get; set; } = new List<AttractionImage>();
        [Required]
        public AttractionType Type { get; set; }
        [Required]
        public virtual City City { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Рейтинг можно вычислять на основе отзывов
        [NotMapped]
        public double Rating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
    }
}
