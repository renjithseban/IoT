using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BusinessEntities
{
    public class WeatherMeasurements
    {
        public string DeviceId { get; set; }

        public string MeasuredDate { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public string SensorType { get; set; }

        public List<AtomicMeasurements> Measurements { get; set; }

        public WeatherMeasurements(string deviceId, string measuredDate, string sensorType)
        {
            try
            {
                this.DeviceId = deviceId;
                this.MeasuredDate = measuredDate;
                this.SensorType = sensorType;//(Sensor)Enum.Parse(typeof(Sensor), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(sensorType.ToLower()));
                this.Measurements = new List<AtomicMeasurements>();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} sensor type is not added in the service", sensorType));
            }
            
        }
    }

    //public enum Sensor
    //{
    //    Temperature,
    //    Humidity,
    //    Rainfall
    //}
}
