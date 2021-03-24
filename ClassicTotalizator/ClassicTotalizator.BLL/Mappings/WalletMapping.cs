using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class WalletMapping
    {
        public static WalletDTO Map(Wallet wallet)
        {
            return wallet == null
                ? null
                : new WalletDTO
                {
                    Amount = wallet.Amount
                };
        }
        
        public static Wallet Map(WalletDTO walletDTO)
        {
            return walletDTO == null
                ? null
                : new Wallet
                {
                    Amount = walletDTO.Amount
                };
        }
    }
}