using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class WalletMapping
    {
        public static WalletDTO Map(Wallet obj)
        {
            return obj == null
                ? null
                : new WalletDTO
                {
                    Amount = obj.Amount
                };
        }
        
        public static Wallet Map(WalletDTO obj)
        {
            return obj == null
                ? null
                : new Wallet
                {
                    Amount = obj.Amount
                };
        }
    }
}