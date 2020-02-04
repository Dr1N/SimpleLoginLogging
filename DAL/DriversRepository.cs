using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    class DriversRepository : IDriversRepository
    {
        #region Fields

        private DbContext _context;

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
            throw new NotImplementedException();
        }

        #endregion
    }
}