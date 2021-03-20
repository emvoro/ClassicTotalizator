﻿using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Mappings
{
    public  static class ParticipantsMapper
    {
        public static ParticipantDTO Map(Participant obj)
        {
            return obj == null
                ? null
                : new ParticipantDTO
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Photo = obj.Photo,
                    Players = obj.Players.Select(PlayerMapper.Map).ToList()
                };
        }
    }
}