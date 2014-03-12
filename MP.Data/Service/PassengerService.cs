using System.Collections.Generic;
using MP.Data.Infrastructure;
using MP.Data.Repository;
using MP.Model.Models;
using MP.Models;

namespace MP.Data.Service
{
    public interface IPassengerService
    {
        IEnumerable<Passenger> GetPassengers();
        IEnumerable<Passenger> GetPassengers(TripModel trip);
        bool AddOrUpdatePassenger(Passenger passenger);
    }
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository passengerRepository;
        private readonly IUnitOfWork unitOfWork;

        public PassengerService(IPassengerRepository passengerRepository, IUnitOfWork unitOfWork)
        {
            this.passengerRepository = passengerRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Passenger> GetPassengers()
        {
            var passengers = passengerRepository.GetAll();
            return passengers;
        }

        public IEnumerable<Passenger> GetPassengers(TripModel trip)
        {
            var passengers = passengerRepository.GetMany(a => a.Trip.DepartureDate == trip.DepartureDate && a.Trip.DepartureTime == trip.DepartureTime);
            return passengers;
        }

        public bool AddOrUpdatePassenger(Passenger passenger)
        {
            passengerRepository.Add(passenger);
            unitOfWork.Commit();
            return true;
        }
    }
}
