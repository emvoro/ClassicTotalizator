﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassicTotalizator.DAL.Entities
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        public int Sport_Id { get; set; }

        [ForeignKey("Sport_Id")]
        public Sport Sport { get; set; }

        public Participant Participant1 { get; set; }

        public Participant Participant2 { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public bool IsEnded { get; set; }

        public BetPool BetPool { get; set; }

        public string[] PossibleResults { get; set; }

        public string Result { get; set; }

        public decimal Margin { get; set; }
    }
}
