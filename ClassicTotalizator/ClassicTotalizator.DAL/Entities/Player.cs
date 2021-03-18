using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassicTotalizator.DAL.Entities
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }

        public Guid Participant_Id { get; set; }

        [ForeignKey("Participant_Id")]
        public virtual Participant Participant { get; set; }

        public string Name { get; set; }

        public decimal Height { get; set; }

        public string Role { get; set; }

        public DateTimeOffset DOB { get; set; }
    }
}
