using Microsoft.EntityFrameworkCore;
 
namespace planner.Models
{
    public class PlannerContext : DbContext
    {
        public DbSet<User> Users  { get; set; }
        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<Rsvp> Rsvps { get; set; }
                // base() calls the parent class' constructor passing the "options" parameter along
        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options) { }
    }
}
