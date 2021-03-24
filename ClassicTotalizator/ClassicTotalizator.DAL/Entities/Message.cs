using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassicTotalizator.DAL.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid Account_Id { get; set; }

        [ForeignKey("Account_Id")]
        public virtual Account Account { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
