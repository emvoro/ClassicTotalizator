using System;

namespace ClassicTotalizator.BLL.Contracts.AccountDTOs
{
    public class AccountInfoDTO
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string AvatarLink { get; set; }
    }
}
