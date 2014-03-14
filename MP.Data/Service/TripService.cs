using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MP.Data.Infrastructure;
using MP.Data.Repository;
using MP.Model.Models;

namespace MP.Data.Service
{
    public interface ITripService
    {
        IEnumerable<Trip> GetTrips();
        Trip AddOrUpdateTripFollowDepartureInfo(Trip trip);
    }
    public class TripService : ITripService
    {
        private readonly ITripRepository tripRepository;
        private readonly IUnitOfWork unitOfWork;

        public TripService(ITripRepository tripRepository, IUnitOfWork unitOfWork)
        {
            this.tripRepository = tripRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Trip> GetTrips()
        {
            var trips = tripRepository.GetAll();
            return trips;
        }

        public Trip AddOrUpdateTripFollowDepartureInfo(Trip trip)
        {
            var existedTrip = tripRepository.GetMany(
                a => a.DepartureDate == trip.DepartureDate && a.DepartureTime == trip.DepartureTime).FirstOrDefault();
            if (existedTrip != null)
            {
                existedTrip.Description = trip.Description;
                existedTrip.DriverName = trip.DriverName;
                existedTrip.DriverPhone = trip.DriverPhone;
                tripRepository.Update(existedTrip);
                unitOfWork.Commit();
                return existedTrip;
            }
            else
            {
                tripRepository.Add(trip);
                unitOfWork.Commit();
                return trip;
            }
        }
    }
}
