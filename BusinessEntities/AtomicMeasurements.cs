using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class AtomicMeasurements
    {
        public DateTime MeasuredDateTime { get; set; }

        public double MeasuredValue { get; set; }

        public AtomicMeasurements(string measuredDateTime, string measuredValue)
        {
            try
            {
                this.MeasuredDateTime = DateTime.Parse(measuredDateTime);
                this.MeasuredValue = Double.Parse(measuredValue.Replace(',', '.'));
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format("Invalid format in the file content: {0} - {1}", measuredDateTime, measuredValue));
            }
        }
    }
}
