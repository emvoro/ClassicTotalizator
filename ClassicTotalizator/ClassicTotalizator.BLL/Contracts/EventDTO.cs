using ClassicTotalizator.DAL.Entities;
using System;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public SportDTO Sport { get; set; }

        [Required]
        public decimal Margin { get; set; }

        [Required]
        public string[] PossibleResults { get; set; }

        [Required]
        public bool IsEnded { get; set; }
        
        [Required]
        public string EventResult { get; set; }

    }
}
