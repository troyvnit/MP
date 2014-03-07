using System.Data.Entity;
using MP.Model.Models;

namespace MP.Data
{
    public class MPEntities : DbContext
    {
        public MPEntities() : base("MPEntities")
        {
        }

        DbSet<Trip> Trips { get; set; }
        DbSet<Passenger> Passengers { get; set; }
        DbSet<Item> Items { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
