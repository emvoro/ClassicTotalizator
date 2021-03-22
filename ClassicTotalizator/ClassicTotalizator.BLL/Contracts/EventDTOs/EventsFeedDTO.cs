using System.Collections.Generic;

namespace ClassicTotalizator.BLL.Contracts.EventDTOs
{
    public class EventsFeedDTO
    {
        public IEnumerable<EventPreviewDTO> Events { get; set; }

        public EventsFeedDTO()
        {
            Events = new List<EventPreviewDTO>();
        }
    }
}