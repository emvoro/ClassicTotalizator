using System;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.DAL.Entities
{
   public class Event
    {
        [Key]
        public Guid Id { get; set; }

        public string Sport { get; set; }

        public Participant Participant1 { get; set; }

        public Participant Participant2 { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }
        
        public string EventImage { get; set; }
        
        public BetPool BetPool { get; set; }
        
        public string EventResult { get; set; }
    }
}
