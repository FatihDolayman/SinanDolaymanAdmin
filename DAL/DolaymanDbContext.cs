
using Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DolaymanDbContext :IdentityDbContext <DolaymanUser>
    {
        public DolaymanDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static DolaymanDbContext Create()
        {
            return new DolaymanDbContext();
        }

        public DbSet<Article> Articles { get; set; }


        public DbSet<Comment> Comments { get; set; }

        public DbSet<Book> Books { get; set; }


        public DbSet<Interview> Interviews { get; set; }


        public DbSet<Sound> Sounds { get; set; }

        public DbSet<SoundCategory> SoundCategories { get; set; }


        public DbSet<Video> Videos { get; set; }

        public DbSet<VideoCategory> VideoCategories { get; set; }

    }
}
