using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Contracts
{
    public class ParticipantsDTO
    {
        public IEnumerable<ParticipantDTO> Participants { get; set; }

        public ParticipantsDTO()
        {
            Participants = new List<ParticipantDTO>();
        }
    }
}
