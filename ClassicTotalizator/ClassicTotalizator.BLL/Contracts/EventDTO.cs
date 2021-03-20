using ClassicTotalizator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.BLL.Contracts
{
    public class EventDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Participant Participant1 { get; set; }

        [Required]
        public Participant Participant2 { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        public string Sport { get; set; }

        public decimal Margin { get; set; }

        public int[] PossibleResults { get; set; }
    }
}
