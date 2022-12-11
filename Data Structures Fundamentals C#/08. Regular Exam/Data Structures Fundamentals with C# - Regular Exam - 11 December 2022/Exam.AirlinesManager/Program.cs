using Exam.DeliveriesManager;
using System;

namespace Exam.AirlinesManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var airlinesManager = new DeliveriesManager.AirlinesManager();

            Airline airline = new Airline("a1", "a2", 4);
            Airline airline2 = new Airline("a2", "a1", 4);
            Airline airline3 = new Airline("a3", "a3", 4);
            Airline airline4 = new Airline("a4", "a4", 10);
            Airline airline5 = new Airline("a5", "a5", 7);

            Flight flight1 = new Flight("a1", "d", "a", "b", false);
            Flight flight2 = new Flight("a2", "c", "a", "b", true);
            Flight flight3 = new Flight("a3", "b", "a", "b", true);
            Flight flight4 = new Flight("a4", "a", "a", "b", false);

            airlinesManager.AddAirline(airline);
            airlinesManager.AddAirline(airline2);
            airlinesManager.AddAirline(airline3);
            airlinesManager.AddAirline(airline4);
            airlinesManager.AddAirline(airline5);
            airlinesManager.AddFlight(airline, flight1);
            airlinesManager.AddFlight(airline, flight2);
            airlinesManager.AddFlight(airline2, flight3);
            airlinesManager.AddFlight(airline2, flight4);

            foreach (var item in airlinesManager.GetAirlinesWithFlightsFromOriginToDestination("a", "b"))
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
