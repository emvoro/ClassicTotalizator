using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClassicTotalizator.BLL.Contracts.PlayerDTOs;

namespace ClassicTotalizator.BLL.Contracts.ParticipantDTOs
{
    public class ParticipantRegisterDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhotoLink { get; set; }

        [Required]
        public IEnumerable<PlayerRegisterDTO> Players { get; set; }

        public IEnumerable<ParameterDTO> Parameters { get; set; }

        public ParticipantRegisterDTO()
        {
            Players = new List<PlayerRegisterDTO>();
            Parameters = new List<ParameterDTO>();
        }
    }
}
