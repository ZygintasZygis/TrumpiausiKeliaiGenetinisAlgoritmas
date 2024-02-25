using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASA_IP
{
    class TaskUtils
    {
        public static List<string> nearestNeighbour(float[,] graph, string[,] names,int start, ref float price) 
        {
            int n = (int)Math.Sqrt(graph.Length);         
            List<string> path = new List<string>();        
            bool[] visited = new bool[n];                  
            int cheapestIndex=0;                           
            visited[start] = true;                        
            path.Add(names[start, start]);                 

            for (int i = start; i < n;)                    
            {                                              
                float smallestPrice = float.MaxValue;           
                visited[i] = true;                         
                for (int j = 0; j < n; j++)                
                {                                          
                    if (graph[i,j]<smallestPrice && visited[j]!=true && graph[i,j]!=0)  
                    {
                        smallestPrice = graph[i, j];       
                        cheapestIndex = j;                 
                    }
                    
                }

                if (visited.Contains(false))              
                {
                    price += graph[i, cheapestIndex];       
                    path.Add(names[i, cheapestIndex]);     
                    i = cheapestIndex;                    
                }
                else                                      
                {
                    price += graph[i, start];               
                    path.Add(names[start, start]);         
                    break;                                 
                }

               

            }

            return path;                                  
        }

        public static float[,] floydWarshall(float[,] graph)
        {
            int V = (int)Math.Sqrt(graph.Length);
            float[,] dist = new float[V, V];
            int i, j, k;

            // Initialize the solution matrix
            // same as input graph matrix
            // Or we can say the initial
            // values of shortest distances
            // are based on shortest paths
            // considering no intermediate
            // vertex
            for (i = 0; i < V; i++)
            {
                for (j = 0; j < V; j++)
                {
                    dist[i, j] = graph[i, j];
                }
            }

            /* Add all vertices one by one to
            the set of intermediate vertices.
            ---> Before start of a iteration,
                 we have shortest distances
                 between all pairs of vertices
                 such that the shortest distances
                 consider only the vertices in
                 set {0, 1, 2, .. k-1} as
                 intermediate vertices.
            ---> After the end of a iteration,
                 vertex no. k is added
                 to the set of intermediate
                 vertices and the set
                 becomes {0, 1, 2, .. k} */
            for (k = 0; k < V; k++)
            {
                // Pick all vertices as source
                // one by one
                for (i = 0; i < V; i++)
                {
                    // Pick all vertices as destination
                    // for the above picked source
                    for (j = 0; j < V; j++)
                    {
                        // If vertex k is on the shortest
                        // path from i to j, then update
                        // the value of dist[i][j]
                        if (dist[i, k] != float.MaxValue && dist[k, j] != float.MaxValue && dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }

            // Print the shortest distance matrix
            printSolution(dist);
            return dist;
        }

       static void printSolution(float[,] dist)
        {
            int V = (int)Math.Sqrt(dist.Length);
            Console.WriteLine(
                "Following matrix shows the shortest "
                + "distances between every pair of vertices");
            for (int i = 0; i < V; ++i)
            {
                for (int j = 0; j < V; ++j)
                {
                    if (dist[i, j] == int.MaxValue)
                    {
                        Console.Write("INF ");
                    }
                    else
                    {
                        Console.Write(dist[i, j] + " ");
                    }
                }

                Console.WriteLine();
            }
        }

        public static List<string> travllingSalesmanProblem(float[,] graph, string[,] names, int s, ref float cheap_path)
        {
            List<int> vertex = new List<int>();
            List<string> cheapestpath = new List<string>();
            List<string> currentpath = new List<string>();
            int V = (int)Math.Sqrt(graph.Length);
            for (int i = 0; i < V; i++)
                if (i != s)
                    vertex.Add(i);

            // store minimum weight
            // Hamiltonian Cycle.
            cheap_path = float.MaxValue;

            do
            {
                // store current Path weight(cost)
                float current_pathweight = 0;
                int k = s;

                // compute current path weight
                for (int i = 0; i < vertex.Count; i++)
                {
                    current_pathweight += graph[k, vertex[i]];
                    k = vertex[i];
                }

                current_pathweight += graph[k, s];

                // update minimum
                cheap_path
                    = Math.Min(cheap_path, current_pathweight);

            } while (findNextPermutation(vertex));

            return cheapestpath;
        }

        // Function to swap the data resent in the left and
        // right indices
        public static List<int> swap(List<int> data, int left,
                                     int right)
        {
            // Swap the data
            int temp = data[left];
            data[left] = data[right];
            data[right] = temp;

            // Return the updated array
            return data;
        }

        // Function to reverse the sub-array starting from left
        // to the right both inclusive
        public static List<int> reverse(List<int> data,
                                        int left, int right)
        {
            // Reverse the sub-array
            while (left < right)
            {
                int temp = data[left];
                data[left++] = data[right];
                data[right--] = temp;
            }

            // Return the updated array
            return data;
        }

        // Function to find the next permutation of the given
        // integer array
        public static bool findNextPermutation(List<int> data)
        {
            // If the given dataset is empty
            // or contains only one element
            // next_permutation is not possible
            if (data.Count <= 1)
                return false;
            int last = data.Count - 2;

            // find the longest non-increasing
            // suffix and find the pivot
            while (last >= 0)
            {
                if (data[last] < data[last + 1])
                    break;
                last--;
            }

            // If there is no increasing pair
            // there is no higher order permutation
            if (last < 0)
                return false;
            int nextGreater = data.Count - 1;

            // Find the rightmost successor
            // to the pivot
            for (int i = data.Count - 1; i > last; i--)
            {
                if (data[i] > data[last])
                {
                    nextGreater = i;
                    break;
                }
            }

            // Swap the successor and
            // the pivot
            data = swap(data, nextGreater, last);

            // Reverse the suffix
            data = reverse(data, last + 1, data.Count - 1);

            // Return true as the
            // next_permutation is done
            return true;
        }

    }
}
