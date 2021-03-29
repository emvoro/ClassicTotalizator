using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClassicTotalizator.BLL.Contracts.PlayerDTOs;

namespace ClassicTotalizator.BLL.Contracts.ParticipantDTOs
{
    public class ParticipantDTO
    {
        [Required]
        public Guid Id { get; set; }
       
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhotoLink { get; set; }

        [Required]
        public ICollection<PlayerDTO> Players { get; set; }

        public ICollection<ParameterDTO> Parameters { get; set; }

        public ParticipantDTO()
        {
            Players = new List<PlayerDTO>();
            Parameters = new List<ParameterDTO>();
        }
    }
}
