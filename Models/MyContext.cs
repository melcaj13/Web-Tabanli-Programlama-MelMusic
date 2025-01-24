using Microsoft.EntityFrameworkCore;
using MelMusic.Models;

    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
         { 
         }
        public DbSet<Gitar> gitarlar { get; set; }
        public DbSet<Keman> kemanlar { get; set; }
        public DbSet<Piyano> piyanolar { get; set; }
        public DbSet<Kalimba> kalimbalar { get; set; }
        public DbSet<Ukulele> ukuleleler { get; set; }
        public DbSet<Mizika> mizikalar { get; set; }
        public DbSet<Plak> plaklar { get; set; }
        public DbSet<Kullanici> kullanicilar { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conStr = "server=LAPTOP-H0KHFLR0\\SQLEXPRESS; uid=sa; password=pass;database=MelMusic; TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(conStr);
        }
    }
