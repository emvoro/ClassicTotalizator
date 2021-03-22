using ClassicTotalizator.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.EventDTOs;
using ClassicTotalizator.BLL.Contracts.SportDTOs;

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
        Task<EventsDTO> GetEventsAsync();

        /// <summary>
        /// Searching for an event in the database
        /// </summary>
        /// <param name="id">Unique identifier of event</param>
        /// <returns> Found event by requested id</returns>
        Task<EventDTO> GetById(Guid id);
        
        /// <summary>
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
        /// Producing list of all sports on the platform
        /// </summary>
        /// <returns>List of current registered sports</returns>
        Task<SportsDTO> GetCurrentListOfSports();

        /// <summary>
        /// Produces list of all not ended events
        /// </summary>
        /// <returns>List of current active events</returns>
        Task<EventsFeedDTO> GetCurrentLineOfEvents();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True  if event closed and bets were calculated; Fasle if smth went wrong</returns>
        Task<bool> FinishEvent(FinishedEventDTO eventToClose);
    }
}
