using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppDron
{
    //TODO: Missing input validation odd orders, parse, number of lines, content, etc ...
    public class Program
    {
        public static void Main(string[] args)
        {
            var allLines = ReadAllLines();
            var area = ParseFlyingArea(allLines[0]);
            var flyingorders = ParseFlightOrders(allLines.Skip(1).ToList());
            var factory = new Func<IFlyingArea, IDron>((a) => new Dron(a));

            flyingorders.ForEach(r =>
            {
                try
                {
                    Console.WriteLine(factory(area).ExecuteFlightOrders(r));
                }
                catch (OutFlyingAreaException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            Console.ReadLine();
        }

        private static List<string> ReadAllLines()
        {
            var exit = false;
            var input = new List<string>();
            do
            {
                var s = Console.ReadLine();
                exit = string.IsNullOrEmpty(s);
                if(!exit)
                    input.Add(s);

            } while (!exit);
            return input;
        }

        private static FlyingArea ParseFlyingArea(string area)
        {
            var values = area.Split(" ").Select(r => r.ChangeType<int>()).ToList();
            return new FlyingArea(values[0], values[1]);
        }

        private static List<FlightOrders> ParseFlightOrders(List<string> flyingOrders)
        {
            var skip = 0;
            var take = 2;
            var keepWorking = true;
            var result = new List<FlightOrders>();
            do
            {
                var flightOrders = flyingOrders.Skip(skip).Take(take).ToList();
                keepWorking = flightOrders.Any();
                if (keepWorking)
                {
                    var positionInfo = flightOrders[0].Split(' ');
                    result.Add(new FlightOrders
                        (
                            new Position(positionInfo[0].ChangeType<int>(), positionInfo[1].ChangeType<int>(), positionInfo[2].ChangeType<CardinalDirection>()),
                            flightOrders[1].Select(r => r.ChangeType<Orders>()).ToList()
                        ));
                }
                skip += take;
            } while (keepWorking);

            return result;
        }
    }
}

