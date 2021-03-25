using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Contracts.BetDTOs
{
    /// <summary>
    /// The contract produced for backoffice, for bet preview
    /// </summary>
    public class BetPreviewForUserDTO
    {
        /// <summary>
        /// Unique identifier of bet
        /// </summary>
        public Guid Bet_Id { get; set; }

        /// <summary>
        /// Full naem of event (Name_participant1 - Name_Participant2)
        /// </summary>
        public string TeamConfrontation { get; set; }

        /// <summary>
        /// User bet choice
        /// </summary>
        public string Choice { get; set; }

        /// <summary>
        /// Time during which the event starts
        /// </summary>
        public DateTimeOffset EventStartime { get; set; }

        /// <summary>
        /// Time of placing a bet
        /// </summary>
        public DateTimeOffset BetTime { get; set; }

        /// <summary>
        /// Amount of money wagered on the current outcome (for this Choice)
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Current status of this bet
        /// </summary>
        public string Status { get; set; }
    }
}
