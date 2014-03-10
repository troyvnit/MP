using MP.Data.Infrastructure;
using MP.Model.Models;

namespace MP.Data.Repository
{
    public class TripRepository : RepositoryBase<Trip>, ITripRepository
    {
        public TripRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ITripRepository : IRepository<Trip>
    {
    }
}
