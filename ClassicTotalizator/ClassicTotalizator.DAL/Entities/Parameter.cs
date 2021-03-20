using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassicTotalizator.DAL.Entities
{
    public class Parameter
    {
        [Key]
        public Guid Id { get; set; }

        public Guid Participant_Id { get; set; }

        [ForeignKey("Participant_Id")]
        public virtual Participant Participant { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}
