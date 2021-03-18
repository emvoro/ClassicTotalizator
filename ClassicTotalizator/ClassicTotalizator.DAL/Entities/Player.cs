using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.DAL.Entities
{
    class Player
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Height { get; set; }

        public string Role { get; set; }
        
        public DateTimeOffset DOB { get; set; }
    }
}
