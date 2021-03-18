﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassicTotalizator.DAL.Entities
{
    class Participant
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }
        
        public string Photo { get; set; }

        public Participant()
        {
            Players = new List<Player>();
        }
    }
}
