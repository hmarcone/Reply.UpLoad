using Microsoft.EntityFrameworkCore;
using Reply.UpLoad.Models;

namespace Reply.UpLoad.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UpLoadImage> UpLoadImages { get; set; }
    }
}
