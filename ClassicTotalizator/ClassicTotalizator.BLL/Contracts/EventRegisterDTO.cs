using ClassicTotalizator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.BLL.Contracts
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
        public int SportId { get; set; }

        [Required]
        public decimal Margin { get; set; }

        [Required]
        public string[] PossibleResults { get; set; }
    }
}
