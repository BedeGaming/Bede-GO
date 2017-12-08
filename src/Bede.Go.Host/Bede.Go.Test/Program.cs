using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Diagnostics;
using Bede.Go.Contracts;
using Bede.Go.Test.Helpers;
using Bede.Go.Test.Enums;
using Bede.Go.Core.Extensions;
using Bede.Go.Core.Helpers;

namespace Bede.Go.Test
{
    public class Program
    {
        private static GeoCoordinate g1 = new GeoCoordinate(54.973841, -1.613155);
        private static GeoCoordinate g2 = new GeoCoordinate(35.689487, 139.691706);

        private static Location l1 = new Location() { Latitude = 54.973841, Longitude = -1.613155 };
        private static Location l2 = new Location() { Latitude = 35.689487, Longitude = 139.691706 };
        
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            ConsoleHelper.Print("*** Go Host Diagnostic Tools ***\n" +
                                "Time to check our functions comrades.\n",
                                MessageTypeEnum.Info);

            ConsoleHelper.Print("+++ Haversine Efficiency Test+++\n" +
                                "First run performance test of haversine functions...\n" +
                                "Ensuring maximum autism.. Press any key to begin.\n",
                                MessageTypeEnum.Info);

            Action defaultGeo = new Action(GetDistance);
            Action fastGeo = new Action(GetCustomDistance);

            Console.ReadKey();

            ConsoleHelper.Print("+++ Default Geo Distance +++\n" +
                                "Elapsed Ticks:",
                                MessageTypeEnum.Info);
            Benchmark(defaultGeo, 10000);

            ConsoleHelper.Print("Distance calculated:" + g1.GetDistanceTo(g2).ToString(),
                                MessageTypeEnum.Info);

            ConsoleHelper.Print("Press any key to continue..\n",
                                MessageTypeEnum.Info);

            Console.ReadKey();

            ConsoleHelper.Print("+++ Custom Geo Distance +++\n" +
                                "Elapsed Ticks:",
                                MessageTypeEnum.Info);
            Benchmark(fastGeo, 10000);

            ConsoleHelper.Print("Distance calculated:" + DistanceHelper.GetDistanceBetween(l1, l2),
                                MessageTypeEnum.Info);

            ConsoleHelper.Print("Press any key to continue..\n",
                                MessageTypeEnum.Info);

            Console.ReadKey();

            ConsoleHelper.Print("+++ Checking uniform point placement +++\n" +
                                "Outputting generated points values for inspection...\n" +
                                "Needs expanding..\n" +
                                "Coordinates:",
                                MessageTypeEnum.Info);

            OutputGameLocationPositions();
            
            ConsoleHelper.Print("Press any key to continue..\n",
                                MessageTypeEnum.Info);

            Console.ReadKey();

            Environment.Exit(500);
        }

        private static void GetDistance()
        {
            var distance = g1.GetDistanceTo(g2);
        }
        public static void GetCustomDistance()
        {
            DistanceHelper.GetDistanceBetween(l1,l2);
        }
        
        private static void Benchmark(Action act, int iterations)
        {
            GC.Collect();
            act.Invoke(); // run once outside of loop to avoid initialization costs
            List<long> results = new List<long>(); //store results

            int count = 1;

            for (int i = 0; i < iterations; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                act.Invoke();
                sw.Stop();

                results.Add(sw.ElapsedTicks);

                ConsoleHelper.DrawProgressBar(count, iterations);
                count++;
            }

            var average = (results.Sum(x => Convert.ToInt32(x)) / iterations).ToString();

            ConsoleHelper.Print("\n\n" + average + "\n", MessageTypeEnum.Info);
        }

        private static void OutputGameLocationPositions()
        {
            var points = GameLocationsHelper.generateGameLocations(l1,6);
            var count = 0;

            foreach (var p in points)
            {
                ConsoleHelper.Print("+++POINT " + count.ToString() + "+++\n"
                                    + "Latitude:" + p.Latitude + "\n"
                                    + "Longitude:" + p.Longitude + "\n"
                                    + "Distance From L1: " + DistanceHelper.GetDistanceBetween(l1, new Location() { Latitude = p.Latitude,
                                        Longitude = p.Longitude
                                    }).ToString()
                                    + "\n",
                                    MessageTypeEnum.Info);
                count++;
            }            
        }
            
    }
}
