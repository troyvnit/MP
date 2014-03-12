using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MP.Data.Infrastructure;
using MP.Data.Repository;
using MP.Model.Models;
using MP.Models;

namespace MP.Data.Service
{
    public interface ITripService
    {
        IEnumerable<Trip> GetTrips();
        Trip AddOrUpdateTripFollowDepartureInfo(TripModel trip);
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

        public Trip AddOrUpdateTripFollowDepartureInfo(TripModel tripModel)
        {
            var existedTrip = tripRepository.GetMany(
                a => a.DepartureDate == tripModel.DepartureDate && a.DepartureTime == tripModel.DepartureTime).FirstOrDefault();
            if (existedTrip != null)
            {
                existedTrip.Description = tripModel.Description;
                existedTrip.DriverName = tripModel.DriverName;
                existedTrip.DriverPhone = tripModel.DriverPhone;
                tripRepository.Update(existedTrip);
                unitOfWork.Commit();
                return existedTrip;
            }
            else
            {
                var trip = Mapper.Map<TripModel, Trip>(tripModel);
                tripRepository.Add(trip);
                unitOfWork.Commit();
                return trip;
            }
        }
    }
}
