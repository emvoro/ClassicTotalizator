using ClassicTotalizator.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Services
{
    /// <summary>
    /// Account auth service abstraction.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Registers user and assigns unique account id.
        /// </summary>
        /// <param name="registerDto">Contract for registration.</param>
        /// <returns>Returns jwt token or <c>null</c> if login already existed.</returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null.</exception>
        Task<bool> CreateEventAsync(EventDTO eventDTO);


    }
}
