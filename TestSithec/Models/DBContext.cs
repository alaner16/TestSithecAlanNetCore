using Microsoft.EntityFrameworkCore;

namespace TestSithec.Models
{
    public class DBContext: DbContext
    {
        public DBContext() { }
        public DBContext(DbContextOptions<DBContext> options) : base(options) 
        { }
        public virtual DbSet<Human> Human { get; set; }
    }
}
