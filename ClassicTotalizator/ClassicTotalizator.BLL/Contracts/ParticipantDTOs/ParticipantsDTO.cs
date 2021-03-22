using System.Collections.Generic;

namespace ClassicTotalizator.BLL.Contracts.ParticipantDTOs
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
