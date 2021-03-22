using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Contracts.ParticipantDTOs
{
    public class ParticipantRegisterDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IEnumerable<PlayerDTO> Players { get; set; }

        [Required]
        public string PhotoLink { get; set; }

        public IEnumerable<ParameterDTO> Parameters { get; set; }

        public ParticipantRegisterDTO()
        {
            Players = new List<PlayerDTO>();
            Parameters = new List<ParameterDTO>();
        }
    }
}
