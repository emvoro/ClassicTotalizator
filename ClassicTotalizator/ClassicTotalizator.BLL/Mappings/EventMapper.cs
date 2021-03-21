using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class EventMapper
    {
        public static Event Map(EventDTO eventDTO)
        {
            return eventDTO == null
                ? null
                : new Event
                {
                    Id = eventDTO.Id,
                    StartTime = eventDTO.StartTime,
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
                    StartTime = registerDTO.StartTime,
                    Margin = registerDTO.Margin,
                    PossibleResults = registerDTO.PossibleResults
                };
        }

        public static EventDTO Map(Event @event)
        {
            return new EventDTO
                {
                    Id = @event.Id,
                    Participant_Id1 = @event.Participant_Id1,
                    Participant_Id2 = @event.Participant_Id2,
                    StartTime = @event.StartTime,
                    Sport_Id = @event.Sport_Id,
                    EventResult = @event.Result,
                    IsEnded = @event.IsEnded,
                    Margin = @event.Margin,
                    PossibleResults = @event.PossibleResults
                };
        }
    }
}
