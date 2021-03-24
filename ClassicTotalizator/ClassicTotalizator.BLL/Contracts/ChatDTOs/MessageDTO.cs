using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Contracts.ChatDTOs
{
    public class MessageDTO
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string Username { get; set; }

        public Guid Account_Id { get; set; }

        public string AvatarLink { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
