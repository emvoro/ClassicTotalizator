using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts
{
    public class ParameterDTO
    {
        [Required]
        public string Type { get; set; }
    
        [Required]
        public string Value { get; set; }
    }
}
