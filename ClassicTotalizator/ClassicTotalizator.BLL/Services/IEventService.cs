using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassicTotalizator.BLL.Services
{
    /// <summary>
    /// Account auth service abstraction.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Creates event for backoffice.
        /// </summary>
        /// <param name="eventDTO">Contract for event.</param>
        /// <returns>Returns jwt token or <c>null</c> if login already existed.</returns>
        Task<EventDTO> CreateEventAsync(EventRegisterDTO eventDTO);

        /// <summary>
        /// Registers user and assigns unique account id.
        /// </summary>
        /// <returns>Returns jwt token or <c>null</c> if login already existed.</returns>
        Task<IEnumerable<EventDTO>> GetEventsAsync();

        /// <summary>
        /// Searching for an event in the database
        /// </summary>
        /// <param name="id">Unique identifier of event</param>
        /// <returns> Found event by requested id</returns>
        Task<EventDTO> GetById(Guid id);
        /// Creates event for backoffice.
        /// </summary>
        /// <param name="sport">Contract for event.</param>
        /// <returns>Returns jwt token or <c>null</c> if login already existed.</returns>
        /// <exception cref="ArgumentNullException">Throws when one of the arguments is null.</exception>
        Task<IEnumerable<EventDTO>> GetEventsBySportAsync(string sport);

        /// <summary>
        /// Trying to edit already created event
        /// </summary>
        /// <param name="newEvent">New variant of this event</param>
        /// <returns>Edited event</returns>
        Task<EventDTO> EditEventAsync(EventDTO newEvent);

        /// <summary>
        /// Producing list of all sports on the platfrom
        /// </summary>
        /// <returns>List of current registered sports</returns>
        Task<SportsDTO> GetCurrentListOfSports();

    }
}
