using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    class Road
    {
        public string Destination { get; set; }
        public int Time { get; set; }
        public float Cost { get; set; }

        public Road(string destination, int time, float cost)
        {
            Destination = destination;
            Time = time;
            Cost = cost;
        }

        public override bool Equals(object obj)
        {
            if (Destination==obj)
            {
                return true;
            }
            return false;
        }
    }
}
