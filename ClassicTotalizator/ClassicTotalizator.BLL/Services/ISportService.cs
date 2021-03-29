using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.SportDTOs;

namespace ClassicTotalizator.BLL.Services
{
    /// <summary>
    /// Adds sports
    /// </summary>
    public interface ISportService
    {
        /// <summary>
        /// Creates new sport.
        /// </summary>
        /// /// <param name="sportDto">Contract for event.</param>
        /// <returns>Boolean result of sport creation</returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null.</exception>
        Task<SportDTO> AddAsync(SportDTO sportDto);

        /// <summary>
        /// Producing list of all sports on the platform
        /// </summary>
        /// <returns>List of current registered sports</returns>
        Task<SportsDTO> GetCurrentListOfSportsAsync();

        /// <summary>
        /// Creates new sport.
        /// </summary>
        /// /// <param name="id">Contract for event.</param>
        /// <returns>Boolean result of sport creation</returns>
        Task<bool> DeleteSportAsync(int id);
    }
}
