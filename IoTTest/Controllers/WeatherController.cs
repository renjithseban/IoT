using System;
using System.Web.Http;
using BusinessServices;
using Microsoft.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace IoTTest.Controllers
{
    //[ApiVersion("1")]
    [RoutePrefix("api/v1")]
    public class WeatherController : ApiController
    {
        readonly IWeatherServices weatherServices;

        public WeatherController(IWeatherServices weatherServices)
        {
            this.weatherServices = weatherServices;
        }

        [SwaggerOperation("GetAll")]
        [Route("devices/{deviceId}/data/{date}/{sensorType?}")]
        [Route("~/getdata/{deviceId=deviceId}/{date=date}/{sensorType=sensorType}")]
        [Route("~/getdatafordevice/{deviceId=deviceId}/{date=date}")]
        public string Get(string deviceId, string date, string sensorType = "")
        {
            try
            {
                //DateTime measuredDate = DateTime.Parse(date);
                var weatherInfo = weatherServices.GetWeatherMeasurements(deviceId, date, sensorType);
                return Newtonsoft.Json.JsonConvert.SerializeObject(weatherInfo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
