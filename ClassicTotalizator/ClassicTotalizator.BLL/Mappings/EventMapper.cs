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
                    Sport = SportMapper.Map(eventDTO.Sport),
                    Result = eventDTO.EventResult,
                    IsEnded =eventDTO.IsEnded,
                    Margin = eventDTO.Margin,
                    PossibleResults = eventDTO.PossibleResults
                };
        }
        public static Event Map(EventRegisterDTO registerDTO)
        {
            return registerDTO == null
                ? null
                : new Event
                {
                    Participant1 = registerDTO.Participant1,
                    Participant2 = registerDTO.Participant2,
                    StartTime = registerDTO.StartTime,
                    Sport = SportMapper.Map(registerDTO.Sport),
                    Margin = registerDTO.Margin,
                    PossibleResults = registerDTO.PossibleResults
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
                    Sport = SportMapper.Map(@event.Sport),
                    EventResult =@event.Result,
                    IsEnded =@event.IsEnded,
                    Margin = @event.Margin,
                    PossibleResults =@event.PossibleResults
                };
        }
    }
}
