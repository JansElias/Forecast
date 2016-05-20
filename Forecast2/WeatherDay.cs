using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast2
{
    public class WeatherDay
    {

        private string uniqueId;

        public string UniqueId
        {
            get
            {
                return uniqueId;
            }
            set
            {
                if (value != uniqueId)
                {
                    uniqueId = value;
                    
                }
            }
        }



    }
}
