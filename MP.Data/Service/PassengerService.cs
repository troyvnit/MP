using System.Collections.Generic;
using System.Linq;
using MP.Data.Infrastructure;
using MP.Data.Repository;
using MP.Model.Models;

namespace MP.Data.Service
{
    public interface IPassengerService
    {
        Passenger GetPassenger(int id);
        IEnumerable<Passenger> GetPassengers();
        IEnumerable<Passenger> GetPassengers(Trip trip);
        bool AddOrUpdatePassenger(Passenger passenger);
        bool DeletePassenger(Passenger passenger);
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
        public Passenger GetPassenger(int id)
        {
            return passengerRepository.GetById(id);
        }
        public IEnumerable<Passenger> GetPassengers()
        {
            var passengers = passengerRepository.GetAll();
            return passengers;
        }

        public IEnumerable<Passenger> GetPassengers(Trip trip)
        {
            var passengers = passengerRepository.GetMany(a => a.Trip.DepartureDate == trip.DepartureDate && a.Trip.DepartureTime == trip.DepartureTime && a.Trip.TripName == trip.TripName);
            return passengers;
        }

        public bool AddOrUpdatePassenger(Passenger passenger)
        {
            if (passenger.Id != 0)
            {
                passengerRepository.Update(passenger);
                unitOfWork.Commit();
            }
            else
            {
                passengerRepository.Add(passenger);
                unitOfWork.Commit();
            }
            return true;
        }

        public bool DeletePassenger(Passenger passenger)
        {
            passengerRepository.Delete(passenger);
            unitOfWork.Commit();
            return true;
        }
    }
}
