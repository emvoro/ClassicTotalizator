using ClassicTotalizator.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Services
{
    /// <summary>
    /// Participants service abstraction
    /// </summary>
    public interface IParticipantsService
    {
        /// <summary>
        /// Creates list of all cuurent participants on the platform
        /// </summary>
        /// <returns>Returns list of current participants or <c>null</c> if no participants on the platform </returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null.</exception>
        Task<ParticipantsDTO> GetAllParticipantsAsync();
    }
}
