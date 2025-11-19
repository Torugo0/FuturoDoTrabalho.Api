using FuturoDoTrabalho.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuturoDoTrabalho.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Trilha> Trilhas => Set<Trilha>();
        public DbSet<Competencia> Competencias => Set<Competencia>();
        public DbSet<Matricula> Matriculas => Set<Matricula>();
        public DbSet<TrilhaCompetencia> TrilhasCompetencias => Set<TrilhaCompetencia>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Usuario)
                .WithMany(u => u.Matriculas)
                .HasForeignKey(m => m.UsuarioId);

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Trilha)
                .WithMany(t => t.Matriculas)
                .HasForeignKey(m => m.TrilhaId);

            modelBuilder.Entity<TrilhaCompetencia>()
                .HasKey(tc => new { tc.TrilhaId, tc.CompetenciaId });

            modelBuilder.Entity<Competencia>().HasData(
                new Competencia { Id = 1, Nome = "Inteligência Artificial", Descricao = "Fundamentos de IA e Machine Learning" },
                new Competencia { Id = 2, Nome = "Análise de Dados", Descricao = "Data Analytics e tomada de decisão baseada em dados" },
                new Competencia { Id = 3, Nome = "Soft Skills", Descricao = "Empatia, colaboração e comunicação" }
            );

            modelBuilder.Entity<Trilha>().HasData(
                new Trilha
                {
                    Id = 1,
                    Nome = "Trilha de IA para Negócios",
                    Descricao = "Introdução à IA aplicada ao contexto corporativo",
                    Nivel = "INICIANTE",
                    CargaHoraria = 40,
                    FocoPrincipal = "IA"
                },
                new Trilha
                {
                    Id = 2,
                    Nome = "Trilha de Dados para Tomada de Decisão",
                    Descricao = "Analytics, métricas e storytelling de dados",
                    Nivel = "INTERMEDIARIO",
                    CargaHoraria = 60,
                    FocoPrincipal = "Dados"
                }
            );

            modelBuilder.Entity<TrilhaCompetencia>().HasData(
                new TrilhaCompetencia { TrilhaId = 1, CompetenciaId = 1 },
                new TrilhaCompetencia { TrilhaId = 1, CompetenciaId = 3 },
                new TrilhaCompetencia { TrilhaId = 2, CompetenciaId = 2 },
                new TrilhaCompetencia { TrilhaId = 2, CompetenciaId = 3 }
            );
        }

    }
}