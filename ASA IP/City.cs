using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    class City
    {
        public string Name { get; set; }
       // public List<Road> Roads { get; set; }

        public List<Road> roads { get; set; }
        public int Visits { get; set; }

        public City(string name)
        {
            Name = name;
            roads = new List<Road>();
            Visits = 0;
        }
        public City()
        {
            Visits = 0;
        }
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is City city &&
                   Name == city.Name &&
                   Visits == city.Visits;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
