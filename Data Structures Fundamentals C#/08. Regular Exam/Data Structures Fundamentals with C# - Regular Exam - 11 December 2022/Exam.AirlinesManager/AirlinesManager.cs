using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class AirlinesManager : IAirlinesManager
    {
        private Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();

        private Dictionary<string, Flight> flights = new Dictionary<string, Flight>();

        public void AddAirline(Airline airline)
        {
            if (!Contains(airline))
            {
                airlines.Add(airline.Id, airline);
            }
        }

        public void AddFlight(Airline airline, Flight flight)
        {
            if (!Contains(airline))
            {
                throw new ArgumentException();
            }

            if (!Contains(flight))
            {
                flights.Add(flight.Id, flight);
            }

            flight.Airline = airline;
            airlines[airline.Id].Flights.Add(flight);
        }

        public bool Contains(Airline airline)
        => airlines.ContainsKey(airline.Id);

        public bool Contains(Flight flight)
        => flights.ContainsKey(flight.Id);

        public void DeleteAirline(Airline airline)
        {
            if (!Contains(airline))
            {
                throw new ArgumentException();
            }

            foreach (var flight in airline.Flights)
            {
                flights.Remove(flight.Id);
            }

            airlines.Remove(airline.Id);
        }

        public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
        => airlines.Values.OrderByDescending(x => x.Rating).ThenByDescending(x => x.Flights.Count).ThenBy(x => x.Name);

        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination)
            => flights.Values
                      .Where(x => !x.IsCompleted && x.Destination == destination && x.Origin == origin)
                      .Select(x => x.Airline);

        public IEnumerable<Flight> GetAllFlights()
        => flights.Values;

        public IEnumerable<Flight> GetCompletedFlights()
        => flights.Values.Where(x => x.IsCompleted);

        public IEnumerable<Flight> GetFlightsOrderedByCompletionThenByNumber()
        => flights.Values.OrderBy(x => x.IsCompleted).ThenBy(x => x.Number);

        public Flight PerformFlight(Airline airline, Flight flight)
        {
            if(!Contains(airline) || !Contains(flight))
            {
                throw new ArgumentException();
            }

            flights[flight.Id].IsCompleted = true;
            flights[flight.Id].Airline = airlines[airline.Id];
            return flights[flight.Id];
        }
    }
}
