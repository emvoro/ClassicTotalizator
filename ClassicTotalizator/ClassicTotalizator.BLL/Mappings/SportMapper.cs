using ClassicTotalizator.BLL.Contracts.SportDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class SportMapper
    {
        public static SportDTO Map(Sport sport)
        {
            return sport == null
                ? null
                : new SportDTO
                {
                    Id = sport.Id,
                    Name = sport.Name
                };
        }

        public static Sport Map(SportDTO sportDTO)
        {
            return sportDTO == null
                ? null
                : new Sport
                {
                    Id = sportDTO.Id,
                    Name = sportDTO.Name
                };
        }
    }
}
