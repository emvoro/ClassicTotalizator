using System.Collections.Generic;

namespace ClassicTotalizator.BLL.Contracts.BetDTOs
{
    public class BetsUserHistoryDTO
    {
        public IEnumerable<BetPreviewForUserDTO> BetsPreviewForUsers { get; set; }
        
        public BetsUserHistoryDTO()
        {
            BetsPreviewForUsers = new List<BetPreviewForUserDTO>();
        }
    }
}
