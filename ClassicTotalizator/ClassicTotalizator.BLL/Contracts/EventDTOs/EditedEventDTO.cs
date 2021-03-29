using System;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts.EventDTOs
{
    public class EditedEventDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public decimal Margin { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }
    }
}
