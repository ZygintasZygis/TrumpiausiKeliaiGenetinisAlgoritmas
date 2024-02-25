using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ASA_IP
{
    class Program
    {
        static void Main(string[] args)
        {
            int startCity = 1;
            List<string> cities = new List<string>();
            List<City> data = InOut.ReadData("places_data5.csv", ref cities);
            data = establishStart(data, startCity, ref cities);
            float[,] matrix = GenerateCostMatrix(data);
            string[,] names = GenerateNameMatrix(data);
            float cheapest = 0;

            //Console.WriteLine("Calculated optimal routes");
            //Console.WriteLine();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            float[,] optimal = TaskUtils.floydWarshall(matrix);

            stopwatch.Start();
            List<string> paths = TaskUtils.travllingSalesmanProblem(optimal, names, 0, ref cheapest);
            stopwatch.Stop();
            Console.WriteLine("BruteForce aproach. Time elapsed : " + stopwatch.ElapsedMilliseconds / 1000);
            Console.WriteLine();
            Console.WriteLine(cheapest);
            Console.WriteLine();
            stopwatch.Reset();


            Console.WriteLine("NearestNeigbour aproach");
            Console.WriteLine();
            cheapest = 0;

            paths = TaskUtils.nearestNeighbour(optimal, names, 0, ref cheapest);
            stopwatch.Stop();
            Console.WriteLine(cheapest);
            Console.WriteLine("Path:");
            Console.WriteLine();
            foreach (var path in paths)
            {
                Console.Write("--> " + path);
            }
            Console.WriteLine();
            Console.WriteLine("Nearest neighbor aproach. Time elapsed : " + stopwatch.ElapsedMilliseconds / 1000);
            Console.WriteLine();
            stopwatch.Reset();

            Console.WriteLine("Genetic algoritm aproach");
            int V = data.Count;
            string genes = GetGenes(V);
            var gn = new Genetic(V, genes, 0, optimal, 50);
            string resultGene = "";
            stopwatch.Start();
            Console.WriteLine(gn.TSP(ref resultGene));
            stopwatch.Stop();
            PrintPath(resultGene, cities);
            Console.WriteLine("Genetic aproach. Time elapsed : " + stopwatch.ElapsedMilliseconds / 1000);

        }

        static string GetGenes(int n) 
        {
            string genes = new string("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            return genes.Substring(0, n - 1);
        
        }

        static List<City> establishStart(List<City> cities, int start, ref List<string> names) 
        {
            City temp = cities[start];
            string hold = names[start];
            names[start] = names[0];
            names[0] = hold;
            cities[start] = cities[0];
            cities[0] = temp;
            return cities;
        
        }

        static void PrintPath(string gene,List<string> cities) 
        {
            foreach (var item in gene)
            {
                Console.Write("--> "+ cities[item-48]);
            }
            Console.WriteLine();
        
        }


        static float[,] GenerateCostMatrix(List<City> cities)
        {
            int size = cities.Count;
            float[,] costMatrix = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        costMatrix[i, j] = 0; // Cost from a city to itself is 0
                    }
                    else
                    {
                        City city1 = cities[i];
                        City city2 = cities[j];

                        Road road = city1.roads.FirstOrDefault(r => r.Destination == city2.Name);

                        if (road != null)
                        {
                            costMatrix[i, j] = road.Cost;
                        }
                        else
                        {
                            // Handle case when road connection is missing between cities
                            // You can choose to assign a high cost value for invalid connections
                            costMatrix[i, j] = float.MaxValue; // Assign a high value for invalid connections
                        }
                    }
                }
            }

            return costMatrix;
        }

        static string[,] GenerateNameMatrix(List<City> cities)
        {
            int size = cities.Count;
            string[,] cityMatrix = new string[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    cityMatrix[i, j] = cities[j].Name;
                }

            }
            return cityMatrix;
        }
    }
}
