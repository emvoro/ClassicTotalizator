using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
