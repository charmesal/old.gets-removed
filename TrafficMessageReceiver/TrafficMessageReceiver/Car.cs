using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficMessageReceiver2
{
    class Car
    {
        public string Licenseplate { get; private set; }
        public string Fueltype { get; private set; }
        public string CarInfo { get; private set; }

        public Car (string licenseplate, string fueltype, string carinfo)
        {
            Licenseplate = licenseplate;
            Fueltype = fueltype;
            CarInfo = carinfo;
        }

        public override string ToString()
        {
            return Licenseplate + "," + Fueltype + "," + CarInfo;
        }
    }
}
