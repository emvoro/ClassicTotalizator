using System;

namespace ClassicTotalizator.BLL.Contracts
{
    public class TransactionDTO
    {
        public Guid Account_Id { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}