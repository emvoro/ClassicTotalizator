using System;

namespace ClassicTotalizator.BLL.Contracts
{
    public class TransactionDTO
    {
        public decimal Amount { get; set; }

        public string Type { get; set; }
    }
}