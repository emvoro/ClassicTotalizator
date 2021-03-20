using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.DAL.Entities
{
    public class Participant
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Player> Players { get; set; }
        
        public string PhotoLink { get; set; }

        public IEnumerable<Parameter> Parameters { get; set; }

        public Participant()
        {
            Players = new List<Player>();
        }
    }
}
