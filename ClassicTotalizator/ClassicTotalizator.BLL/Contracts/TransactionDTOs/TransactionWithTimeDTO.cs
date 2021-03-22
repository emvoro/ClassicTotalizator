using System;

namespace ClassicTotalizator.BLL.Contracts.TransactionDTOs
{
    public class TransactionWithTimeDTO
    {
        public decimal Amount { get; set; }

        public string Type { get; set; }
        
        public DateTimeOffset DateTime { get; set; }
    }
}