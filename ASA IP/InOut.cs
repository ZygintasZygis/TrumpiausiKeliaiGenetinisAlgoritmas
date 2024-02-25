using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    class InOut
    {
        public static List<City> ReadData(string fileName, ref List<string> cities) 
        {
            List<City> data = new List<City>();
            string[] lines = File.ReadAllLines(fileName);
            int cnt = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] values = line.Split(',');
                int time = int.Parse(values[2]);
                float cost = 0;
                float.TryParse(values[3], out cost);
                City city = new City(values[0]);
                Road road = new Road(values[1], time, cost);
                if (!data.Contains(city))
                {
                    data.Add(city);
                    cities.Add(city.Name);
                    data[cnt].roads.Add(road);
                    cnt++;
                }
                else
                {
                    data[data.IndexOf(city)].roads.Add(road);
                }
                

            }
           
            return data;
        }
        
    }
}
