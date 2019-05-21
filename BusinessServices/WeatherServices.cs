using BusinessEntities;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    class WeatherServices : IWeatherServices
    {
        readonly ICloudStorageAccess cloudStorageAccess;

        public WeatherServices(ICloudStorageAccess cloudStorageAccess)
        {
            this.cloudStorageAccess = cloudStorageAccess;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.cloudStorageAccess != null)
                {
                    this.cloudStorageAccess.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<WeatherMeasurements> GetWeatherMeasurements(string deviceId, string measuredDate, string sensorType = "")
        {
            try
            {
                List<WeatherMeasurements> lstWeatherMeasurements = new List<WeatherMeasurements>();
                if (string.IsNullOrEmpty(sensorType))
                {
                    //foreach(var sensor in Enum.GetValues(typeof(Sensor)))
                    string[] sensors = ConfigurationManager.AppSettings["SensorType"].Split(',');
                    foreach(string sensor in sensors)
                    {
                        var weatherInfo = cloudStorageAccess.GetWeatherMeasurements(deviceId, measuredDate, sensor.ToString().ToLower());
                        lstWeatherMeasurements.Add(weatherInfo);
                    }
                }
                else
                {
                    var weatherInfo = cloudStorageAccess.GetWeatherMeasurements(deviceId, measuredDate, sensorType);
                    lstWeatherMeasurements.Add(weatherInfo);
                }
                
                return lstWeatherMeasurements;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
