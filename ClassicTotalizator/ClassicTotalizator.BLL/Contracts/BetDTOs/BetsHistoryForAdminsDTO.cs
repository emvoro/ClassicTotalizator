using System.Collections.Generic;

namespace ClassicTotalizator.BLL.Contracts.BetDTOs
{
    public class BetsHistoryForAdminsDTO
    {

        public IEnumerable<BetPreviewForAdminsDTO> BetsPreviewForAdmins { get; set; }
       
        public BetsHistoryForAdminsDTO()
        {
            BetsPreviewForAdmins = new List<BetPreviewForAdminsDTO>();
        }

    }
}
