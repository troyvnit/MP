using MP.Data.Infrastructure;
using MP.Model.Models;

namespace MP.Data.Repository
{
    public class PassengerRepository : RepositoryBase<Passenger>, IPassengerRepository
    {
        public PassengerRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }

    public interface IPassengerRepository : IRepository<Passenger>
    {
    }
}
