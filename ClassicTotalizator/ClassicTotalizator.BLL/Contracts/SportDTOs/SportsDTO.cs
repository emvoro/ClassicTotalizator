using System.Collections.Generic;

namespace ClassicTotalizator.BLL.Contracts.SportDTOs
{
    public class SportsDTO
    {
        public IEnumerable<SportDTO> Sports { get; set; }

        public SportsDTO()
        {
            Sports = new List<SportDTO>();
        }
    }
}
