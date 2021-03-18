using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClassicTotalizator.DAL.Entities
{
    class Bet
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid User_Id { get; set; }

        [ForeignKey("User_Id")]
        public virtual Account Account { get; set; }

        [Required]
        public Guid Event_Id { get; set; }



        [Required]
        public string Choice { get; set; }

        [Required]
        public decimal Amount { get; set; }

    }
}
