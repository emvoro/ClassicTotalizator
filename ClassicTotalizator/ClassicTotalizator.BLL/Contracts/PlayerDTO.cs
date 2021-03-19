using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.BLL.Contracts
{
    public class PlayerDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid Participant_Id { get; set; }

        [Required]
        public ParticipantDTO Participant { get; set; }

        [Required]
        public string Name { get; set; }

        public Dictionary<string, int> Statistics { get; set; }

        public PlayerDTO()
        {
            Statistics = new Dictionary<string, int>();
        }
    }
}
