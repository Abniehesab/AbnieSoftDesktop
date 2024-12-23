using Domain;
using Domain.Entities.ACC;

using Domain.Entities.FIN;

using Domain.Entities.PRO;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class AbnieSoftDbContext : DbContext
    {
        public AbnieSoftDbContext()
        {
        }


        #region constructor

        public AbnieSoftDbContext(DbContextOptions<AbnieSoftDbContext> options) : base(options)
        {
        }

        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"Data Source=D:\AbnieSoft\AbnieSoftDatabase.db; Cache=Shared";
                optionsBuilder.UseSqlite(connectionString);
                Console.WriteLine($"Using SQLite database at: {connectionString}");
            }
        }



        #region Db Sets          
        public DbSet<Kol> Kol { get; set; }
        public DbSet<Moein> Moein { get; set; }
        public DbSet<Tafzili> Tafzili { get; set; }
        public DbSet<Tafzili2> Tafzili2 { get; set; }
        public DbSet<Tafzili3> Tafzili3 { get; set; }
        public DbSet<TafziliGroup> TafziliGroup { get; set; }
        public DbSet<TafziliType> TafziliType { get; set; }
        public DbSet<MoeinTafziliGroups> MoeinTafziliGroups { get; set; }
        public DbSet<AccDocmentDetails> AccDocmentDetails { get; set; }

        public DbSet<Tender> Tender { get; set; }

        public DbSet<Contract> Contract { get; set; }


        public DbSet<Material> Material { get; set; }
        public DbSet<MaterialGroup> MaterialGroup { get; set; }
        public DbSet<MaterialUnit> MaterialUnit { get; set; }

        public DbSet<ProjectStatusFactor> ProjectStatusFactor { get; set; }
        public DbSet<CostList> CostList { get; set; }
        public DbSet<CostListDetails> CostListDetails { get; set; }


        public DbSet<PaymentCheque> PaymentCheque { get; set; }


        public DbSet<ReceiveCheque> ReceiveCheque { get; set; }


        public DbSet<Store> Store { get; set; }

        public DbSet<MaterialCirculation> MaterialCirculation { get; set; }

        #endregion





        #region disable cascading delete in database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {






            var cascades = modelBuilder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetForeignKeys())
                    .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.SetNull);

            foreach (var fk in cascades)
            {
                fk.DeleteBehavior = DeleteBehavior.SetNull;
            }

            base.OnModelCreating(modelBuilder);

        }
        #endregion

        public void EnableForeignKeys()
        {
            this.Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON;");
        }

        public bool TestConnection()
        {
            try
            {
                // اجرای یک query ساده برای تست اتصال
                this.Database.CanConnect();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}

