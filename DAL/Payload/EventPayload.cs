using System;
using System.Text.Json.Serialization;

namespace DAL.Payload
{
    public class EventPayload
    {
        [JsonPropertyName("EventTimestamp")]
        public DateTime EventTimestamp { get; private set; }

        [JsonPropertyName("Driver ID")]
        public int DriverId { get; private set; }

        public EventPayload(DateTime time, int driverId)
        {
            EventTimestamp = time;
            DriverId = driverId;
        }
    }
}