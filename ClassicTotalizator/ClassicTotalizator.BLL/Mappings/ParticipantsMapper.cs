using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;
using System.Linq;

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
                    PhotoLink = obj.PhotoLink,
                    Players = obj.Players.Select(PlayerMapper.Map).ToList()
                };
        }
        public static Participant Map(ParticipantDTO obj)
        {
            return obj == null
                       ? null
                       : new Participant
                       {
                           Id = obj.Id,
                           Name = obj.Name,
                           PhotoLink = obj.PhotoLink,
                           Players = obj.Players.Select(PlayerMapper.Map).ToList()
                       };
        }
    } 
}
