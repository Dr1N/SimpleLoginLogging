using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDriversRepository : IDisposable
    {
        #region Drivers

        public IEnumerable<Driver> GetDrivers();

        public Task AddDriverAsync(Driver driver);

        public Task ClearDriversAsync();

        #endregion

        #region Events

        public IEnumerable<Event> GetEvents();

        public Task AddEventAsync(Event evnt);
        
        public Task ClearEventsAsync();

        #endregion
    }
}