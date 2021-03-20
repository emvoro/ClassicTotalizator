using ClassicTotalizator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassicTotalizator.BLL.Contracts
{
    public class EventDTO
    {
        public Participant Participant1 { get; set; }

        public Participant Participant2 { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public string Sport { get; set; }

        public decimal Margin { get; set; }

        public int[] PossibleResults { get; set; }
    }
}
