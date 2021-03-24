using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class PlayerMapper
    {
        public static PlayerDTO Map(Player player)
        {
            return player == null
                ? null
                : new PlayerDTO
                {
                   Id = player.Id,
                   Participant_Id = player.Participant_Id,
                   Name = player.Name
                };
        }

        public static Player Map(PlayerDTO playerDTO)
        {
            return playerDTO == null
                ? null
                : new Player
                {
                    Id = playerDTO.Id,
                    Participant_Id = playerDTO.Participant_Id,
                    Name = playerDTO.Name
                };
        }

        public static Player Map(PlayerRegisterDTO playerRegisterDTO)
        {
            return playerRegisterDTO == null
                ? null
                : new Player
                {
                    Name = playerRegisterDTO.Name
                };
        }
    }
}
