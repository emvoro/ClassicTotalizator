using ClassicTotalizator.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// /// <param name="sportDTO">Contract for event.</param>
        /// <returns>Boolean result of sport creation</returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null.</exception>
        Task<SportDTO> Add(SportDTO sportDTO);
    }
}
