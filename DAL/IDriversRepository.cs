using DAL.Models;
using System;

namespace DAL
{
    public interface IDriversRepository : IDisposable
    {
        public void AddDriver(Driver driver);
    }
}