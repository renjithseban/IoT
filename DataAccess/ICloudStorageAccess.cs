using BusinessEntities;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public interface ICloudStorageAccess : IDisposable
    {
        WeatherMeasurements GetWeatherMeasurements(string deviceId, string measuredDate, string sensorType);
    }
}
