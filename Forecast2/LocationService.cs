using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Forecast2
{
    public class LocationService
    {
        public bool IsLocationServiceEnabled
        {
            get
            {
                Geolocator locationservice = new Geolocator();
                if (locationservice.LocationStatus == PositionStatus.Disabled)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
