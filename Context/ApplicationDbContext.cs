using automationTest.Models;
using Microsoft.EntityFrameworkCore;

namespace automationTest.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<tblElasticData> tblElasticDatas { get; set; }
        public DbSet<tblEvent> tblEvents { get; set; }
        //public class ElasticContext : DbContext
        //{
        //    public DbSet<tblElasticData> tblElasticDatas { get; set; }
        //    public ElasticContext(DbContextOptions<ElasticContext> options) : base(options)
        //    {
        //    }
        //}
        //public class EventContext : DbContext
        //{
        //    public DbSet<tblEvent> tblEvents { get; set; }
        //    public EventContext(DbContextOptions<EventContext> options) : base(options)
        //    {
        //    }
        //}
    }
}
