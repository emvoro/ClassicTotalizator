using ClassicTotalizator.DAL.Entities;
using System.Linq;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;

namespace ClassicTotalizator.BLL.Mappings
{
    public  static class ParticipantsMapper
    {
        public static ParticipantDTO Map(Participant participant)
        {
            return participant == null
                ? null
                : new ParticipantDTO
                {
                    Id = participant.Id,
                    Name = participant.Name,
                    PhotoLink = participant.PhotoLink,
                    Players = participant.Players.Select(PlayerMapper.Map).ToList()
                };
        }

        public static Participant Map(ParticipantRegisterDTO participantRegisterDTO)
        {
            return participantRegisterDTO == null
                ? null
                : new Participant
                {
                    Name = participantRegisterDTO.Name,
                    PhotoLink = participantRegisterDTO.PhotoLink,
                    Players = participantRegisterDTO.Players.Select(PlayerMapper.Map).ToList()
                };
        }

        public static Participant Map(ParticipantDTO participantDTO)
        {
            return participantDTO == null
                ? null
                : new Participant
                {
                    Id = participantDTO.Id,
                    Name = participantDTO.Name,
                    PhotoLink = participantDTO.PhotoLink,
                    Players = participantDTO.Players.Select(PlayerMapper.Map).ToList()
                };
        }
    } 
}
