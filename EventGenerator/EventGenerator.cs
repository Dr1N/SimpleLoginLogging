using DAL;
using DAL.Models;
using DAL.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EventGenerator
{
    class EventGenerator : IDisposable
    {
        #region Constants

        private const string ENDPOINT = "http://localhost:5000/events";
        private const string CONTENT_TYPE = "application/json";

        #endregion

        #region Fields

        private readonly CancellationTokenSource _tokenSource;

        private readonly string _connectionString;

        private IEnumerable<Driver> _drivers = null;

        #endregion

        #region Properties

        public IEnumerable<Driver> Drivers 
        { 
            get
            {
                if (_drivers == null)
                {
                    _drivers = GetDrivers();
                }

                return _drivers;
            }
        }

        #endregion

        #region Life

        public EventGenerator(string connString)
        {
            if (string.IsNullOrEmpty(connString))
            {
                throw new ArgumentException(nameof(connString));
            }
            _connectionString = connString;
            _tokenSource = new CancellationTokenSource();
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tokenSource?.Dispose();
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

        #region Public Methods

        public async void Start()
        {
            if (Drivers.Count() == 0)
            {
                Console.WriteLine("Drivers table is empty!");
                return;
            }

            var token = _tokenSource.Token;
            while (!token.IsCancellationRequested)
            {
                foreach (var driver in Drivers)
                {
                    try
                    {
                        _tokenSource.Token.ThrowIfCancellationRequested();
                        var success = await SendLoginEventRequestAsync(GenerateEventForDriver(driver), _tokenSource.Token);
                        if (success)
                        {
                            Console.WriteLine($"Driver: { driver.Id }\tsuccess");
                        }
                        else
                        {
                            Console.WriteLine($"Driver: { driver.Id }\terror");
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error. Send event request: { ex.Message }");
                    }
                }
            }
        }

        public void Stop()
        {
            try
            {
                _tokenSource?.Cancel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error. Cancel work: { ex.Message }");
            }
        }

        #endregion

        #region Private Methods

        private IEnumerable<Driver> GetDrivers()
        {
            var result = new List<Driver>();
            try
            {
                using var repo = new DriversRepository(_connectionString);
                result.AddRange(repo.GetDrivers());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error. Read drivers error: { ex.Message }");
            }

            return result;
        }

        private EventPayload GenerateEventForDriver(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            return new EventPayload(DateTime.Now, driver.Id);
        }

        private async Task<bool> SendLoginEventRequestAsync(EventPayload eventPayload, CancellationToken token)
        {
            if (eventPayload == null)
            {
                throw new ArgumentNullException(nameof(eventPayload));
            }

            var success = false;
            try
            {
                var jsonString = JsonSerializer.Serialize(eventPayload);
                using var httpClient = new HttpClient();
                using var response = await httpClient.PostAsync(ENDPOINT, new StringContent(jsonString, Encoding.UTF8, CONTENT_TYPE), token);
                success = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Http Error: { ex.Message }");
            }

            return success;
        }

        #endregion
    }
}