using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.ParticipantDTOs;

namespace ClassicTotalizator.BLL.Services
{
    /// <summary>
    /// Participants service abstraction
    /// </summary>
    public interface IParticipantsService
    {
        /// <summary>
        /// Create list of all participants on the platform.
        /// </summary>
        /// <returns>Returns list of current participants or <c>null</c> if no participants on the platform </returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null.</exception>
        Task<ParticipantsDTO> GetAllParticipantsAsync();

        /// <summary>
        /// Add new participant.
        /// </summary>
        /// <returns>Returns true if Participant was added, false if smth went wrong</returns>
        Task<ParticipantDTO> AddNewParticipantAsync(ParticipantRegisterDTO participant);

        /// <summary>
        /// Delete participant.
        /// </summary>
        /// <returns>Returns true if Participant was deleted, false if there is no participant with such Id.</returns>
        Task<bool> DeleteParticipantAsync(Guid id);
    }
}
