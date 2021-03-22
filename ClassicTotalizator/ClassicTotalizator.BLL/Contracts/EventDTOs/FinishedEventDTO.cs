using System;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts.EventDTOs
{
    public class FinishedEventDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Result { get; set; }
    }
}
