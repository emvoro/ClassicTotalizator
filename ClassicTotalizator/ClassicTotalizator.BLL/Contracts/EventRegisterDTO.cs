﻿using ClassicTotalizator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.BLL.Contracts
{
    public class EventRegisterDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Participant Participant1 { get; set; }

        [Required]
        public Participant Participant2 { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        [Required]
        public string Sport { get; set; }

        [Required]
        public decimal Margin { get; set; }

        [Required]
        public int[] PossibleResults { get; set; }
    }
}
