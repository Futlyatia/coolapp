using coolapp.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace coolapp.Models
{
    public class AppCtx : IdentityDbContext<User>
    {
        public AppCtx(DbContextOptions<AppCtx> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<EditorsForAlbums> EditorsForAlbums { get; set;}
    }
}
