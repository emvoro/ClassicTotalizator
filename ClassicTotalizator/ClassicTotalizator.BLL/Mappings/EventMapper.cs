using ClassicTotalizator.BLL.Contracts.EventDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class EventMapper
    {
        public static Event Map(EventDTO obj)
        {
            return obj == null
                ? null
                : new Event
                {
                    Id = obj.Id,
                    StartTime = obj.StartTime,
                    Result = obj.EventResult,
                    IsEnded = obj.IsEnded,
                    Margin = obj.Margin,
                    PossibleResults = obj.PossibleResults
                };
        }

        public static Event Map(EventRegisterDTO obj)
        {
            return obj == null
                ? null
                : new Event
                {
                    StartTime = obj.StartTime,
                    Margin = obj.Margin,
                    PossibleResults = obj.PossibleResults
                };
        }

        public static EventPreviewDTO MapPreview(Event obj)
        {
            return obj == null
                ? null
                : new EventPreviewDTO
                {
                    Id = obj.Id,
                    Participant1 = ParticipantsMapper.Map(obj.Participant1),
                    Participant2 = ParticipantsMapper.Map(obj.Participant2),
                    StartTime = obj.StartTime,
                    SportName = obj.Sport.Name,
                    IsEnded = obj.IsEnded,
                    Margin = obj.Margin,
                    PossibleResults = obj.PossibleResults
                };
        }

        public static EventDTO Map(Event obj)
        {
            return obj == null
                ? null
                : new EventDTO
                {
                    Id = obj.Id,
                    Participant_Id1 = obj.Participant_Id1,
                    Participant_Id2 = obj.Participant_Id2,
                    StartTime = obj.StartTime,
                    Sport_Id = obj.Sport_Id,
                    EventResult = obj.Result,
                    IsEnded = obj.IsEnded,
                    Margin = obj.Margin,
                    PossibleResults = obj.PossibleResults
                };
        }
    }
}
