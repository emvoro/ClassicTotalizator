using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassicTotalizator.DAL.Entities
{
    public class Bet
    {
        [Key]
        public Guid Id { get; set; }

        public Guid Account_Id { get; set; }

        [ForeignKey("Account_Id")]
        public virtual Account Account { get; set; }

        public Guid Event_Id { get; set; }

        [ForeignKey("Event_Id")]
        public virtual BetPool BetPool { get; set; }

        public string Choice { get; set; }

        public decimal Amount { get; set; }


        /*
        Part that you can see after this comment needs to be added in new migration
         */
        public DateTimeOffset BetTime { get; set; }

        public string Status { get; set; }
    }
}
