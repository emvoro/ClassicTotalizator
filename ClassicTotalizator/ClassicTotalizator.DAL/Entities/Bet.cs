﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassicTotalizator.DAL.Entities
{
   public  class Bet
    {
        [Key]
        public Guid Id { get; set; }

        public Guid Account_Id { get; set; }

        [ForeignKey("User_Id")]
        public virtual Account Account { get; set; }

        public Guid Event_Id { get; set; }

        [ForeignKey("Event_Id")]
        public virtual BetPool BetPool { get; set; }

        public string Choice { get; set; }

        public decimal Amount { get; set; }
    }
}