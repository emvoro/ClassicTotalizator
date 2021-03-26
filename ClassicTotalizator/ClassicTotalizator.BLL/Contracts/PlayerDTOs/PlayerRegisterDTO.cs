using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts.PlayerDTOs
{
    public class PlayerRegisterDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
