using IDCapstone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FlaggedComment> FlaggedComments { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<PlayerVideo> PlayerVideos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<IDCapstone.Models.UsersList> UsersList { get; set; }
    }
}
