using ClassicTotalizator.BLL.Contracts.PlayerDTOs;
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

        public static Player Map(PlayerRegisterDTO obj)
        {
            return obj == null
                ? null
                : new Player
                {
                    Name = obj.Name
                };
        }
    }
}
