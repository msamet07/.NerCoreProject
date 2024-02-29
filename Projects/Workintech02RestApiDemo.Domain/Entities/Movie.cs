using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workintech02RestApiDemo.Domain.Entities
{
    [Table("Movie",Schema ="APP")]
    public class Movie:BaseEntity
    {
        [Required]
        [MaxLength(500)]
        [MinLength(2)]
        [Column(Order=1)]
        public string Title { get; set; }
        [Column(Order = 2)]
        [Required]
        public int ReleaseYear { get; set; }
        [Column(Order = 3)]

        public bool IsActive { get; set; }
    }
}
