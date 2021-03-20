using System;
using System.Collections.Generic;

namespace ClassicTotalizator.BLL.Contracts
{
    public class WalletDTO
    {
        public Guid Account_Id { get; set; }

        public decimal Amount { get; set; }
    }
}