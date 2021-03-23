﻿using ClassicTotalizator.BLL.Contracts.EventDTOs;
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

        public static EventPreviewDTO MapPreview(Event @event)
        {
            return @event == null
                ? null
                : new EventPreviewDTO
                {
                    Id = @event.Id,
                    Participant1 = ParticipantsMapper.Map(@event.Participant1),
                    Participant2 = ParticipantsMapper.Map(@event.Participant2),
                    StartTime = @event.StartTime,
                    SportName = @event.Sport.Name,
                    IsEnded = @event.IsEnded,
                    Margin = @event.Margin,
                    PossibleResults = @event.PossibleResults
                };
        }


        public static EventDTO Map(Event @event)
        {
            return @event == null
                ? null
                : new EventDTO
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
