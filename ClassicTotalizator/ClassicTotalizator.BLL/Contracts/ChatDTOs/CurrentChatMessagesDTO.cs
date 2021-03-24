using System.Collections.Generic;

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
