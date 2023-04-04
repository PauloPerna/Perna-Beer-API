using Microsoft.EntityFrameworkCore;
using WebApi.Models;    

namespace WebApi.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        //Add DbSet properties for each of your tables here
        public DbSet<Brew> Brews { get; set; }
    }
}