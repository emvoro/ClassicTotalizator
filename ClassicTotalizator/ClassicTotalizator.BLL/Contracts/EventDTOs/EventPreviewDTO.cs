using System;
using System.Collections.Generic;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;

namespace ClassicTotalizator.BLL.Contracts.EventDTOs
{
    public class EventPreviewDTO
    {
        public Guid Id { get; set; }

        public ParticipantDTO Participant1 { get; set; }

        public ParticipantDTO Participant2 { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public string SportName { get; set; }

        public decimal Margin { get; set; }

        public string[] PossibleResults { get; set; }

        public bool IsEnded { get; set; }
        
        public decimal AmountW1 { get; set; }
        
        public decimal AmountW2 { get; set; }
        
        public decimal AmountX { get; set; }
    }
}