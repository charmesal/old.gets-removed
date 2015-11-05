using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCenter
{

    public class Car
    {
        public string Licenseplate { get; private set; }
        public TypeOfFuel Fueltype { get; private set; }
        public string Brand { get; private set; }
        public double FuelCapacity { get; private set; }
        public Owner Owner { get; private set; }

        /// <summary>
        /// Create a car.
        /// </summary>
        /// <param name="licenseplate">The licenseplate of the car.</param>
        /// <param name="fueltype">What kind of fueltype has the car, enum TypeOfFuel.</param>
        /// <param name="brand">Brand of the car.</param>
        /// <param name="fuelcapacity">How much has the fuelcapacity.</param>
        /// <param name="owner">Who own's the car.</param>
        public Car (string licenseplate, TypeOfFuel fueltype, string brand, double fuelcapacity, Owner owner)
        {
            Licenseplate = licenseplate;
            Fueltype     = fueltype;
            Brand        = brand;
            FuelCapacity = fuelcapacity;
            Owner        = owner;
        }

        /// <summary>
        /// Create a car without a owner
        /// </summary>
        /// <param name="licenseplate"></param>
        /// <param name="fueltype"></param>
        /// <param name="brand"></param>
        /// <param name="fuelcapacity"></param>
        public Car(string licenseplate, TypeOfFuel fueltype, string brand, double fuelcapacity)
        {
            Licenseplate = licenseplate;
            Fueltype = fueltype;
            Brand = brand;
            FuelCapacity = fuelcapacity;
            Owner = null;
        }

        public override string ToString()
        {
            return Licenseplate + "  "+ Fueltype.ToString();
        }
    }
}
