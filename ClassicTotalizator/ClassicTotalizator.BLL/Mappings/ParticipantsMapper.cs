using ClassicTotalizator.DAL.Entities;
using System.Linq;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;

namespace ClassicTotalizator.BLL.Mappings
{
    public  static class ParticipantsMapper
    {
        public static ParticipantDTO Map(Participant participant)
        {
            if (participant == null)
                return null;

            var participantDto = new ParticipantDTO
            {
                Id = participant.Id,
                Name = participant.Name,
                PhotoLink = participant.PhotoLink
            };
            
            if (participant.Players != null)
                participantDto.Players = participant.Players.Select(PlayerMapper.Map).ToList();
            if (participant.Parameters != null)
                participantDto.Parameters = participant.Parameters.Select(ParameterMapper.Map).ToList();
            
            return participantDto;
        }

        public static Participant Map(ParticipantRegisterDTO participantRegisterDTO)
        {
            if (participantRegisterDTO == null)
                return null;

            var participantDto = new Participant
            {
                Name = participantRegisterDTO.Name,
                PhotoLink = participantRegisterDTO.PhotoLink
            };
            
            if (participantRegisterDTO.Players != null)
                participantDto.Players = participantRegisterDTO.Players.Select(PlayerMapper.Map).ToList();
            if (participantRegisterDTO.Parameters != null)
                participantDto.Parameters = participantRegisterDTO.Parameters.Select(ParameterMapper.Map).ToList();
            
            return participantDto;
        }

        public static Participant Map(ParticipantDTO participantDTO)
        {
            if (participantDTO == null)
                return null;

            var participant = new Participant
            {
                Id = participantDTO.Id,
                Name = participantDTO.Name,
                PhotoLink = participantDTO.PhotoLink
            };

            if (participantDTO.Players != null)
                participant.Players = participantDTO.Players.Select(PlayerMapper.Map).ToList();

            return participant;
        }
    } 
}
