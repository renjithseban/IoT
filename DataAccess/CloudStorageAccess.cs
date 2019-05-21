using BusinessEntities;
using Microsoft.Azure; //Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DataAccess
{
    class CloudStorageAccess : ICloudStorageAccess
    {
        private CloudStorageAccount storageAccount;

        private const string CONTAINER = "iotbackend";

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (storageAccount != null)
                {
                    storageAccount = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void CreateStorageAccountFromConnectionString()
        {
            try
            {
                storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WeatherMeasurements GetWeatherMeasurements(string deviceId, string measuredDate, string sensorType)
        {
            try
            {
                CreateStorageAccountFromConnectionString();

                // Create file path from deviceId, sensorType and measuredDate
                var prefix = string.Join("/", deviceId, sensorType);
                var fileName = string.Join(".",measuredDate,"csv");
                var path = string.Join("/", prefix, fileName);

                // Create a blob client for interacting with the blob service.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Create a container for organizing blobs within the storage account.
                CloudBlobContainer container = blobClient.GetContainerReference(CONTAINER);

                CloudAppendBlob appendBlob = container.GetAppendBlobReference(path);

                if (appendBlob.Exists())
                {
                    WeatherMeasurements weatherMeasurements = new WeatherMeasurements(deviceId, measuredDate, sensorType);
                    using (var memoryStream = new MemoryStream())
                    {
                        appendBlob.DownloadToStream(memoryStream);
                        memoryStream.Position = 0;
                        var text = Encoding.ASCII.GetString(memoryStream.ToArray());
                        using (var reader = new StreamReader(memoryStream, Encoding.ASCII))
                        {
                            string line = String.Empty;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] val = line.Split(';');
                                AtomicMeasurements atomic = new AtomicMeasurements(val[0], val[1]);
                                weatherMeasurements.Measurements.Add(atomic);
                            }
                        }
                    }
                    return weatherMeasurements;
                }
                else
                {
                    throw new FileNotFoundException();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
