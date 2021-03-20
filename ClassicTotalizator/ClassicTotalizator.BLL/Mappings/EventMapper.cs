using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public class EventMapper
    {
        public static Event Map(EventDTO eventDTO)
        {
                return eventDTO == null
                    ? null
                    : new Event
                    {
                        Participant1 = eventDTO.Participant1,
                        Participant2 = eventDTO.Participant2,
                        StartTime = eventDTO.StartTime,
                        Sport = eventDTO.Sport
                    };
        }


        public static EventDTO Map(Event @event)

        {
            return @event == null
                ? null
                : new EventDTO
                {
                    Participant1 = @event.Participant1,
                    Participant2 = @event.Participant2,
                    StartTime = @event.StartTime,
                    Sport = @event.Sport
                };
        }
    }
}
