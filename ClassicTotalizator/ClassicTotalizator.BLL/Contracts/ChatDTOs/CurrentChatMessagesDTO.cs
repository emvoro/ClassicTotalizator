using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Contracts.ChatDTOs
{
    public class CurrentChatMessagesDTO
    {
        public IEnumerable<MessageDTO> Messages { get; set; }
        public CurrentChatMessagesDTO()
        {
            Messages = new List<MessageDTO>();
        }
    }
}
