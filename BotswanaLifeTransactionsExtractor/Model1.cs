namespace BotswanaLifeTransactionsExtractor
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=BotsLife")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<PostOffices2> PostOffices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Requests)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.Client_WebServiceClientId);

            modelBuilder.Entity<PostOffices2>()
                .Property(e => e.PostOfficeSK);

            modelBuilder.Entity<PostOffices2>()
                .Property(e => e.PostOfficeName)
                .IsUnicode(false);

            modelBuilder.Entity<PostOffices2>()
                .Property(e => e.IPSCode)
                .IsUnicode(false);

            modelBuilder.Entity<PostOffices2>()
                .Property(e => e.Status)
                .IsUnicode(false);
        }
    }
}
