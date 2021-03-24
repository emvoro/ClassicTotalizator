using System;
using System.ComponentModel.DataAnnotations;

namespace ClassicTotalizator.BLL.Contracts.ChatDTOs
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageToPostDTO
    {
        /// <summary>
        /// Users message to post in message pool
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        public string Text { get; set; }

        [Required(AllowEmptyStrings = false)]
        public DateTimeOffset CreationTime { get; set; }
    }
}
