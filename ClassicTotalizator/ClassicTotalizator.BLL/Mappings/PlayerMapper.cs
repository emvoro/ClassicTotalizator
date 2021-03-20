using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class PlayerMapper
    {

        public static PlayerDTO Map(Player obj)
        {
            return obj == null
                ? null
                : new PlayerDTO
                {
                   Id = obj.Id,
                   Participant_Id = obj.Participant_Id,
                   Name = obj.Name
                };
        }
    }
}
