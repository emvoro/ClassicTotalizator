using System.Collections.Generic;

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
