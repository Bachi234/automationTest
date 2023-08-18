using automationTest.Models;
using Microsoft.EntityFrameworkCore;

namespace automationTest.Context
{
    public class ApplicationDbContext : DbContext
    {
        public class ElasticContext : DbContext
        {
            public ElasticContext(DbContextOptions<ElasticContext> options) : base()
            {
            }
            public DbSet<tblElasticData> tblElasticData { get; set; }
        }
        public class EventContext : DbContext
        {
            public EventContext(DbContextOptions<EventContext> options) : base()
            {
            }
            public DbSet<tblEvent> tblEvent { get; set; }
        }
    }
}
