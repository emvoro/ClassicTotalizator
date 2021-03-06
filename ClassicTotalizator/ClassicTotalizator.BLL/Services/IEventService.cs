using System;
using System.Threading.Tasks;
using ClassicTotalizator.BLL.Contracts.EventDTOs;

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
        /// <param name="eventDto">Contract for event.</param>
        /// <returns>Returns jwt token or <c>null</c> if login already existed.</returns>
        Task<EventDTO> CreateEventAsync(EventRegisterDTO eventDto);

        /// <summary>
        /// Registers user and assigns unique account id.
        /// </summary>
        /// <returns>Returns jwt token or <c>null</c> if login already existed.</returns>
        Task<EventsFeedDTO> GetEventsAsync();

        /// <summary>
        /// Searching for an event in the database
        /// </summary>
        /// <param name="id">Unique identifier of event</param>
        /// <returns> Found event by requested id</returns>
        Task<EventDTO> GetById(Guid id);

        /// <summary>
        /// Trying to edit already created event
        /// </summary>
        /// <param name="newEvent">New variant of this event</param>
        /// <returns>Edited event</returns>
        Task<EventDTO> EditEventAsync(EditedEventDTO newEvent);

        /// <summary>
        /// Produces list of all not ended events
        /// </summary>
        /// <returns>List of current active events</returns>
        Task<EventsFeedDTO> GetCurrentLineOfEventsAsync();

        /// <summary>
        /// Finishing event
        /// </summary>
        /// <returns>True  if event closed and bets were calculated; False if smth went wrong</returns>
        Task<bool> FinishEventAsync(FinishedEventDTO eventToClose);

        /// <summary>
        ///  Searching for an event in the database and backs detailed object
        /// </summary>
        /// <param name="id">Unique identifier of event</param>
        /// <returns>Details</returns>
        Task<EventPreviewDTO> GetEventPreviewAsync(Guid id);

        /// <summary>
        /// Deletes event from database
        /// </summary>
        /// <param name="id">Unique identifier of event</param>
        /// <returns>True if event was deleted; false if event was not deleted </returns>
        Task<bool> DeleteEventAsync(Guid id);
    }
}
