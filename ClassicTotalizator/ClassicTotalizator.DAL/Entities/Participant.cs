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

        public string PhotoLink { get; set; }
        
        public ICollection<Player> Players { get; set; }

        public ICollection<Parameter> Parameters { get; set; }

        public Participant()
        {
            Players = new List<Player>();
            Parameters = new List<Parameter>();
        }
    }
}
