using System;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.DAL.Entities
{
    public class Player
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
