using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Contracts
{
    public class OutcomesDTO
    {
        public IEnumerable<string> Outcomes { get; set; }

        public OutcomesDTO()
        {
            Outcomes = new List<string>() { "W1", "X", "W2" };
        }
    }
}
