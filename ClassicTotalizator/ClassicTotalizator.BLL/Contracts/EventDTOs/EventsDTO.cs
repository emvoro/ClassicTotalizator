using System.Collections.Generic;

namespace ClassicTotalizator.BLL.Contracts.EventDTOs
{
    public class EventsDTO
    {
        public IEnumerable<EventDTO> Events { get; set; }
        public EventsDTO()
        {
            Events = new List<EventDTO>();
        }
        
    }
}
