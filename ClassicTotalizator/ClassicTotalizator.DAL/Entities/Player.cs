﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClassicTotalizator.DAL.Entities
{
    class Player
    {
        [Key]
        public Guid Id { get; set; }

        public Guid Participant_Id { get; set; }

        [ForeignKey("Participant_Id")]
        public virtual Participant Participant { get; set; }

        public string Name { get; set; }

        public decimal Height { get; set; }

        public string Role { get; set; }
        
        public DateTimeOffset DOB { get; set; }
    }
}
