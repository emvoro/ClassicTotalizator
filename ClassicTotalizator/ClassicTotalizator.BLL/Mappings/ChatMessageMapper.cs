using ClassicTotalizator.BLL.Contracts.ChatDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class ChatMessageMapper
    {
        public static Message Map(MessageToPostDTO obj)
        {
            return obj == null
                ? null
                : new Message
                {
                   Text = obj.Text
                };
        }
        
        public static MessageDTO Map(Message obj)
        {
            return obj == null
                ? null
                : new MessageDTO
                {
                    Id = obj.Id,
                    Account_Id = obj.Account_Id,
                    Text = obj.Text,
                    Time = obj.Time
                };
        }
    }
}