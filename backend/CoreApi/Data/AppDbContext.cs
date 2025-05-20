using CoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<PacienteModel> Pacientes { get; set; }
        public DbSet<ConsultaModel> Consultas { get; set; }
        public DbSet<MedicoModel> Medicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsultaModel>()
                .HasOne(c => c.PacienteDaConsulta)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.PacienteId);

            modelBuilder.Entity<ConsultaModel>()
                .HasOne(c => c.MedicoDaConsulta)
                .WithMany(m => m.Consultas)
                .HasForeignKey(c => c.MedicoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
