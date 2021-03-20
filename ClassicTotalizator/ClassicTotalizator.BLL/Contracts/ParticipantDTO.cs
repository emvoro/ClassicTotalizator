using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.BLL.Contracts
{
    public class ParticipantDTO
    {
        [Required]
        public Guid Id { get; set; }
       
        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<PlayerDTO> Players { get; set; }

        [Required]
        public string Photo { get; set; }

        public ParticipantDTO()
        {
            Players = new List<PlayerDTO>();
        }
    }
}
