using BusinessEntities;
using System;
using System.Collections.Generic;

namespace BusinessServices
{
    public interface IWeatherServices
    {
        List<WeatherMeasurements> GetWeatherMeasurements(string deviceId, string measureddate, string sensorType);
    }
}