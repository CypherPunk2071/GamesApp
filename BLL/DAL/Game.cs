using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public bool IsMultiplayer { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number!")]
        public decimal? Price { get; set; }

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }
    }
}
