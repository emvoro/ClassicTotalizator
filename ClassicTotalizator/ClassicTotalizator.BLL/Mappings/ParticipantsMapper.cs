using ClassicTotalizator.DAL.Entities;
using System.Linq;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;

namespace ClassicTotalizator.BLL.Mappings
{
    public  static class ParticipantsMapper
    {
        public static ParticipantDTO Map(Participant obj)
        {
            if (obj == null)
                return null;

            var participantDto = new ParticipantDTO
            {
                Id = obj.Id,
                Name = obj.Name,
                PhotoLink = obj.PhotoLink
            };
            
            if (obj.Players != null)
                participantDto.Players = obj.Players.Select(PlayerMapper.Map).ToList();
            if (obj.Parameters != null)
                participantDto.Parameters = obj.Parameters.Select(ParameterMapper.Map).ToList();
            
            return participantDto;
        }

        public static Participant Map(ParticipantRegisterDTO obj)
        {
            if (obj == null)
                return null;

            var participantDto = new Participant
            {
                Name = obj.Name,
                PhotoLink = obj.PhotoLink
            };
            
            if (obj.Players != null)
                participantDto.Players = obj.Players.Select(PlayerMapper.Map).ToList();
            if (obj.Parameters != null)
                participantDto.Parameters = obj.Parameters.Select(ParameterMapper.Map).ToList();
            
            return participantDto;
        }
    } 
}
