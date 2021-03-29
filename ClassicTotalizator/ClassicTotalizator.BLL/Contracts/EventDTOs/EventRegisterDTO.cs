using System;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts.EventDTOs
{
    public class EventRegisterDTO
    {
        [Required]
        public Guid Participant_Id1 { get; set; }

        [Required]
        public Guid Participant_Id2 { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        [Required]
        public int Sport_Id { get; set; }

        [Required]
        public decimal Margin { get; set; }

        [Required]
        public string[] PossibleResults { get; set; }
    }
}
