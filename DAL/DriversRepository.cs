﻿using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class DriversRepository : IDriversRepository
    {
        #region Fields

        private readonly DriversContext _context;

        #endregion

        #region Life

        public DriversRepository()
        {
            _context = new DriversContext();
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

        public void AddDriver(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }
            _context.Drivers.Add(driver);
            _context.SaveChanges();
        }
        
        public void ClearDrivers()
        {
            _context.Drivers.RemoveRange(_context.Drivers);
            _context.SaveChanges();
#pragma warning disable CS0618 // Type or member is obsolete
            _context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('tblDrivers', RESEED, 0)");
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public void AddEvent(Event evnt)
        {
            throw new NotImplementedException();
        }

        public void ClearEvents()
        {
            _context.Events.RemoveRange(_context.Events);
            _context.SaveChanges();
#pragma warning disable CS0618 // Type or member is obsolete
            _context.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('tblEvents', RESEED, 0)");
#pragma warning restore CS0618 // Type or member is obsolete
        }

        #endregion
    }
}