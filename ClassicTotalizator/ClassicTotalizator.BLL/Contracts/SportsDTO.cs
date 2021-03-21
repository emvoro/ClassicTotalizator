using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Contracts
{
    public class SportsDTO
    {
        public IEnumerable<SportDTO> Sports { get; set; }
    }
}
