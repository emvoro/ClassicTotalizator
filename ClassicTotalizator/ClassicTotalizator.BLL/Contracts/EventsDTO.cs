using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Contracts
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
