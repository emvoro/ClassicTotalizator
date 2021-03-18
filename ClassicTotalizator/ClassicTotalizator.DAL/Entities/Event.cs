using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.DAL.Entities
{
    class Event
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Sport { get; set; }

        [Required]
        public Participant Participant1 { get; set; }

        [Required]
        public Participant Participant2 { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        [Required]
        public DateTimeOffset EndTime { get; set; }
        
        public string EventImage { get; set; }
        
        public BetPool BetPool { get; set; }
        
        public string EventResult { get; set; }
    }
}
