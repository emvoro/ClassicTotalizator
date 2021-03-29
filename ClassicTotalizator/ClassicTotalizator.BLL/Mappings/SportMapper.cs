using ClassicTotalizator.BLL.Contracts.SportDTOs;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class SportMapper
    {
        public static SportDTO Map(Sport obj)
        {
            return obj == null
                ? null
                : new SportDTO
                {
                    Id = obj.Id,
                    Name = obj.Name
                };
        }

        public static Sport Map(SportDTO obj)
        {
            return obj == null
                ? null
                : new Sport
                {
                    Id = obj.Id,
                    Name = obj.Name
                };
        }
    }
}
