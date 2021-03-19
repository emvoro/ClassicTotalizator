using ClassicTotalizator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassicTotalizator.BLL.Contracts
{
    public class ParticipantsDTO
    {
        public IEnumerable<Participant> Participants { get; set; }
        public ParticipantsDTO()
        {
            Participants = new List<Participant>();
        }
    }
}
