using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    public struct Individual 
    {
        public string gene;
        public float fitness;
    }
    class Genetic
    {
        public Genetic(int v, string genes, int start, float[,] data, int popsize)
        {
            V = v;
            GENES = genes;
            START = start;
            costMatrix = data;
            POP_SIZE = popsize;
        }

        static public int V { get; set; }
        static public string GENES { get; set; }
        static public int START { get; set; }

        static public float[,] costMatrix {get;set;}
        static public int POP_SIZE { get; set; }

        

        /// <summary>
        /// RandNumber
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        static int RandNum(int start, int end) 
        {
            int r = end - start;
            int rnum = start + new Random().Next() % r;
            return rnum;
        
        }


        /// <summary>
        /// does string contain char
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        static bool Repeat(string s, char ch) 
        {
            return s.Contains(ch);
        }

        static string MutatedGene(string gnome) 
        {
            while (true)
            {
                int r = RandNum(1, V);
                int r1 = RandNum(1, V);

                if (r1!=r)
                {
                    char[] arr = gnome.ToCharArray();
                    char temp = arr[r];
                    arr[r] = arr[r1];
                    arr[r1] = temp;
                    gnome = new string(arr);
                    break;
                }
            }
            return gnome;
        
        }

        static string CreateGnome() 
        {
            string gnome = "0";
            while (true)
            {
                if (gnome.Length==V)
                {
                    gnome += gnome[0];
                    break;
                }
                int temp = RandNum(1, V);

                if (!Repeat(gnome, (char)(temp + 48)))
                    gnome += (char)(temp + 48);
            }
            return gnome;

        }

        static float CallFitness(string gnome) 
        {
            float[,] map = costMatrix;
            float f = 0;
            for (int i = 0; i < gnome.Length-1; i++)
            {
                if (map[gnome[i]-48,gnome[i+1]-48]==float.MaxValue)
                {
                    return float.MaxValue;
                }
                f += map[gnome[i] - 48, gnome[i + 1] - 48];
            }

            return f;
        }

        static int CoolDown(int temp)
        {
            return (90 * temp) / 100;
        }

        static bool LessThan(Individual t1, Individual t2)
        {
            return t1.fitness < t2.fitness;
        }

       public float TSP(ref string genome) 
        {
            int gen = 1;
            int gen_thresh=V*2;
            List<Individual> population = new List<Individual>();
            float result = 0;
            Individual temp;

            // Initialize population
            for (int i = 0; i < POP_SIZE; i++)
            {
                temp.gene = CreateGnome();
                temp.fitness = CallFitness(temp.gene);
                population.Add(temp);
            }

            bool found = false;
            int temperature = 10000;

            while (temperature > 1000 && gen<=gen_thresh)
            {
                population = population.OrderBy(x => x.fitness).ToList();
                List<Individual> new_population = new List<Individual>();
                for (int i = 0; i < POP_SIZE; i++)
                {
                    Individual p1 = population[i];
                    while (true)
                    {
                        string new_gene = MutatedGene(p1.gene);
                        Individual new_gnome;
                        new_gnome.gene = new_gene;
                        new_gnome.fitness = CallFitness(new_gnome.gene);
                        if (new_gnome.fitness<=population[i].fitness)
                        {
                            new_population.Add(new_gnome);
                            break;
                        }
                        else
                        {
                            float prob = (float)Math.Pow(2.7,-1 * ((float)(new_gnome.fitness - population[i].fitness)/ temperature));
                            if (prob > 0.5)
                            {
                                new_population.Add(new_gnome);
                                break;
                            }
                        }
                    }
                }
                temperature = CoolDown(temperature);
                population = new_population;
                
                    population = population.OrderBy(x => x.fitness).ToList();
                    genome = population[0].gene;
                    result= population[0].fitness;
              
               
                gen++;
            }
            return result;
        }


    }
}
