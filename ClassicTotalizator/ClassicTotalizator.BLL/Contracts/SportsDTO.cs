﻿using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts
{
    public class SportsDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
