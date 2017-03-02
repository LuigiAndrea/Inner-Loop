//Example: Print all positive integer solutions to the equation a3  +  b3 = c3  + d3 
// where a, b, c, and d are integers between 1 and n. 

using System;
using static System.Console;
using System.Collections.Generic;
using System.Diagnostics;

namespace EquationApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const int nanosec = 1000000;
            const int number = 150;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            int sol = equationWithHashTable(number);
            sw.Stop();
            WriteLine($"{nameof(equationWithHashTable)} takes {Math.Floor(sw.Elapsed.TotalSeconds)} seconds and {sw.Elapsed.Milliseconds * nanosec} nanoseconds");
            WriteLine($"The number of solutions is {sol}");

            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            sol = equationOneValidD(number);
            sw2.Stop();
            WriteLine($"{nameof(equationOneValidD)} takes {Math.Floor(sw2.Elapsed.TotalSeconds)} seconds and {sw2.Elapsed.Milliseconds * nanosec} nanoseconds");
            WriteLine($"The number of solutions is {sol}");

            Stopwatch sw3 = new Stopwatch();
            sw3.Start();
            sol = equationBruteForce(number);
            sw3.Stop();
            WriteLine($"{nameof(equationBruteForce)} takes {Math.Floor(sw3.Elapsed.TotalSeconds)} seconds and {sw3.Elapsed.Milliseconds * nanosec} nanoseconds");
            WriteLine($"The number of solutions is {sol}");
        }

        public static int equationWithHashTable(int size)
        {
            int dim = size;
            Dictionary<double, List<string>> mapTable = new Dictionary<double, List<string>>();
            double res;

            for (int a = 0; a < dim; a++)
            {
                for (int b = 0; b < dim; b++)
                {
                    res = Math.Pow(a, 3) + Math.Pow(b, 3);
                    if (mapTable.ContainsKey(res))
                        mapTable[res].Add($"({a},{b})");
                    else
                        mapTable.Add(res, new List<string>() { $"({a},{b})" });
                }
            }
            int sol = 0;
            foreach (KeyValuePair<double, List<string>> map in mapTable)
            {
                foreach (string pair_ab in map.Value)
                {
                    foreach (string pair_cd in map.Value)
                    {
                        // WriteLine($"{pair_ab} --> {pair_cd}");
                        sol++;
                    }
                }
            }
            return sol;
        }

        public static int equationOneValidD(int size)
        {
            int dim = size;
            List<string> list = new List<string>();
            double res, res2, cP, d;

            for (int a = 0; a < dim; a++)
            {
                for (int b = 0; b < dim; b++)
                {
                    for (int c = 0; c < dim; c++)
                    {

                        res = Math.Pow(a, 3) + Math.Pow(b, 3);
                        cP = Math.Pow(c, 3);
                        d = Math.Round(Math.Pow(Math.Abs(res - cP), (double)1 / 3));

                        if (d >= dim) continue;

                        res2 = cP + Math.Pow(d, 3);

                        if (res == res2)
                        {
                            list.Add($"({a},{b}) --> ({c},{d})");
                            //  WriteLine($"({a},{b}) --> ({c},{d})");
                        }
                    }
                }
            }

            return list.Count;
        }

        public static int equationBruteForce(int size)
        {
            int dim = size;
            List<string> list = new List<string>();
            double res, res2;

            for (int a = 0; a < dim; a++)
            {
                for (int b = 0; b < dim; b++)
                {
                    for (int c = 0; c < dim; c++)
                    {
                        for (int d = 0; d < dim; d++)
                        {

                            res = Math.Pow(a, 3) + Math.Pow(b, 3);
                            res2 = Math.Pow(c, 3) + Math.Pow(d, 3);

                            if (res == res2)
                            {
                                list.Add($"({a},{b}) --> ({c},{d})");
                                // WriteLine($"({a},{b}) --> ({c},{d})");
                            }
                        }
                    }
                }
            }

            return list.Count;
        }
    }
}
