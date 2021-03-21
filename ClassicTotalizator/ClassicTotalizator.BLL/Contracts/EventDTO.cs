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
        public Guid ParticipantId1 { get; set; }

        [Required]
        public Guid ParticipantId2 { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        [Required]
        public int SportId { get; set; }

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
