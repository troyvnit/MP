using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP.Data.Repository;
using MP.Model.Models;

namespace MP.Data.Service
{
    public interface ITripService
    {
        IEnumerable<Trip> GetTrips();
    }
    public class TripService : ITripService
    {
        private readonly ITripRepository tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }

        public IEnumerable<Trip> GetTrips()
        {
            var trips = tripRepository.GetAll();
            return trips;
        } 
    }
}
