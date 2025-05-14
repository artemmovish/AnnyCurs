using AdminApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdminApp.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public UserType Type { get; set; }
        public virtual List<Attraction> FavoriteAttractions { get; set; } = new List<Attraction>();

        public virtual List<Review> Reviews { get; set; } = new List<Review>();
    }
}
