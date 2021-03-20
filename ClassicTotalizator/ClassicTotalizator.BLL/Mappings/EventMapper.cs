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
                    Id = eventDTO.Id,
                    Participant1 = eventDTO.Participant1,
                    Participant2 = eventDTO.Participant2,
                    StartTime = eventDTO.StartTime,
                    Sport = eventDTO.Sport
                    //EventResult =@event.EventResult,
                    //IsEnded =@event.IsEnded,
                    //Margin = @event.Margin,
                    //PossibleResults =@event.PossibleResults
                };
        }


        public static EventDTO Map(Event @event)
        {
            return @event == null
                ? null
                : new EventDTO
                {
                    Id = @event.Id,
                    Participant1 = @event.Participant1,
                    Participant2 = @event.Participant2,
                    StartTime = @event.StartTime,
                    Sport = @event.Sport
                    //EventResult =@event.EventResult,
                    //IsEnded =@event.IsEnded,
                    //Margin = @event.Margin,
                    //PossibleResults =@event.PossibleResults
                };
        }
    }
}
