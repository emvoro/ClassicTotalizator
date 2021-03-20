﻿using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Mappings
{
    public class EventMapper
    {
        public static Event ToEvent(EventDTO eventDTO)
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
    }
}