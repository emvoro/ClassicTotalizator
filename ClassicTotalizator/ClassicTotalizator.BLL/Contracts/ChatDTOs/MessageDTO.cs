using System;

namespace ClassicTotalizator.BLL.Contracts.ChatDTOs
{
    public class MessageDTO
    {
        public Guid Id { get; set; }

        public Guid Account_Id { get; set; }
        
        public string Text { get; set; }

        public string Username { get; set; }

        public string AvatarLink { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
