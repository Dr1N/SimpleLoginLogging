using DAL.Models;
using System;

namespace DAL
{
    public interface IDriversRepository : IDisposable
    {
        public void AddDriver(Driver driver);

        public void ClearDrivers();

        public void AddEvent(Event evnt);

        public void ClearEvents();
    }
}