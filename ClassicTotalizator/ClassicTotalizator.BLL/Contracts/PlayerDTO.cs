using System;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts
{
    public class PlayerDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Participant_Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
