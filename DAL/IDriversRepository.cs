using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IDriversRepository : IDisposable
    {
        public IEnumerable<Driver> GetDrivers();

        public void AddDriver(Driver driver);

        public void ClearDrivers();

        public IEnumerable<Event> GetEvents();

        public void AddEvent(Event evnt);
        
        public void ClearEvents();
    }
}