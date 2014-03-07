using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MP.Data.Infrastructure;
using MP.Model.Models;

namespace MP.Data.Repository
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IItemRepository : IRepository<Item>
    {
    }
}
