using DAL.Context;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class DriversRepository : IDriversRepository
    {
        #region Fields

        private readonly DriversContext _context;

        #endregion

        #region Life

        public DriversRepository(string connStr)
        {
            _context = new DriversContext(connStr);
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        #endregion

        #region IDriversRepository Implementation

        public IEnumerable<Driver> GetDrivers()
        {
            try
            {
                return _context.Drivers.ToList();
            }
            catch (Exception ex)
            {
                throw new DataLayerException(ex);
            }
        }

        public async Task AddDriverAsync(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }
            try
            {
                await _context.Drivers.AddAsync(driver);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DataLayerException(ex);
            }
        }
        
        public async Task ClearDriversAsync()
        {
            try
            {
                _context.Drivers.RemoveRange(_context.Drivers);
                await _context.SaveChangesAsync();
#pragma warning disable CS0618 // Type or member is obsolete
                await _context.Database.ExecuteSqlCommandAsync($"DBCC CHECKIDENT ('tblDrivers', RESEED, 0)");
#pragma warning restore CS0618 // Type or member is obsolete
            }
            catch (Exception ex)
            {
                throw new DataLayerException(ex);
            }
        }

        public IEnumerable<Event> GetEvents()
        {
            try
            {
                return _context.Events.ToList();
            }
            catch (Exception ex)
            {
                throw new DataLayerException(ex);
            }
        }

        public async Task AddEventAsync(Event evnt)
        {
            if (evnt == null)
            {
                throw new ArgumentNullException(nameof(evnt));
            }

            try
            {
                await _context.Events.AddAsync(evnt);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DataLayerException(ex);
            }
        }

        public async Task ClearEventsAsync()
        {
            try
            {
                _context.Events.RemoveRange(_context.Events);
                await _context.SaveChangesAsync();
#pragma warning disable CS0618 // Type or member is obsolete
                await _context.Database.ExecuteSqlCommandAsync($"DBCC CHECKIDENT ('tblEvents', RESEED, 0)");
#pragma warning restore CS0618 // Type or member is obsolete
            }
            catch (Exception ex)
            {
                throw new DataLayerException(ex);
            }
        }

        #endregion
    }
}