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
        public IEnumerable<PlayerDTO> Players { get; set; }

        [Required]
        public string PhotoLink { get; set; }

        public IEnumerable<ParameterDTO> Parameters { get; set; }

        public ParticipantDTO()
        {
            Players = new List<PlayerDTO>();
            Parameters = new List<ParameterDTO>();
        }
    }
}
