using System;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts.EventDTOs
{
    public class EdittedEventDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        [Required]
        public decimal Margin { get; set; }
    }
}
