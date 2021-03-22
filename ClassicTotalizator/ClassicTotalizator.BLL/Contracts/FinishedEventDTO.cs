using System;

namespace ClassicTotalizator.BLL.Contracts
{
    public class FinishedEventDTO
    {
        public Guid Event_Id { get; set; }

        public string Result { get; set; }
    }
}
