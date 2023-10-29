using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Messaging;
using System.ComponentModel.DataAnnotations;
using TCC.Domain.Models;
using TCC.Infra.Data.Mappings;

namespace TCC.Infra.Data.Context;

public class AppDbContext : IdentityDbContext<
    Usuario,
    ApplicationRole,
    Guid,
    ApplicationUserClaim,
    ApplicationUserRole,
    ApplicationUserLogin,
    ApplicationRoleClaim,
    ApplicationUserToken
    >, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Aula> Aulas { get; set; }
    public DbSet<ItemLoja> Itens { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Exercicio> Exercicios { get; set; }
    public DbSet<PedidoLoja> Pedidos { get; set; }
    public DbSet<ProgressoAula> Progressos { get; set; }
    public DbSet<RespostaAlunoExercicio> RespostasAlunoExercicios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                     e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        #region Mappers
        modelBuilder.ApplyConfiguration(new CursoMap());
        modelBuilder.ApplyConfiguration(new ItemLojaMap());
        modelBuilder.ApplyConfiguration(new AulaMap());
        modelBuilder.ApplyConfiguration(new ExercicioMap());
        modelBuilder.ApplyConfiguration(new PedidoLojaMap());
        modelBuilder.ApplyConfiguration(new UsuarioMap());
        modelBuilder.ApplyConfiguration(new ProgressoAulaMap());
        modelBuilder.ApplyConfiguration(new RespostaAlunoExercicioMap());
        #endregion


        #region populateDB
        GenerateStoreItems(modelBuilder);
        #endregion


        base.OnModelCreating(modelBuilder);
    }

    private void GenerateStoreItems(ModelBuilder modelBuilder)
    {
        var itens = new List<ItemLoja>
        {
            new ItemLoja
            {
                Id = Guid.NewGuid(),
                Nome = "Boost de XP",
                Descricao = "Boost de XP de 75%",
                Preco = 500,
                ImagemUrl = "75-XP.png",
                Duracao = TimeSpan.FromDays(3).Ticks,
                TipoItem = Domain.Enums.TipoItemLoja.Boost,
                Multiplicador = 0.75m
            },
            new ItemLoja
            {
                Id = Guid.NewGuid(),
                Nome = "Boost de XP",
                Descricao = "Boost de XP de 50%",
                Preco = 400,
                ImagemUrl = "50-XP.png",
                Duracao = TimeSpan.FromDays(3).Ticks,
                TipoItem = Domain.Enums.TipoItemLoja.Boost,
                Multiplicador = 0.50m
            },
            new ItemLoja
            {
                Id = Guid.NewGuid(),
                Nome = "Boost de XP",
                Descricao = "Boost de XP de 25%",
                Preco = 300,
                ImagemUrl = "25-XP.png",
                Duracao = TimeSpan.FromDays(3).Ticks,
                TipoItem = Domain.Enums.TipoItemLoja.Boost,
                Multiplicador = 0.25m
            },
            new ItemLoja
            {
                Id = Guid.NewGuid(),
                Nome = "Pacote de XP",
                Descricao = "Receba 250 em XP",
                Preco = 450,
                ImagemUrl = "250-XP.png",
                TipoItem = Domain.Enums.TipoItemLoja.PacoteXp,
                QtdXp = 250
            },
            new ItemLoja
            {
                Id = Guid.NewGuid(),
                Nome = "Pacote de XP",
                Descricao = "Receba 500 em XP",
                Preco = 900,
                ImagemUrl = "500-XP.png",
                TipoItem = Domain.Enums.TipoItemLoja.PacoteXp,
                QtdXp = 500
            },
            new ItemLoja
            {
                Id = Guid.NewGuid(),
                Nome = "Pacote de XP",
                Descricao = "Receba 1000 em XP",
                Preco = 1800,
                ImagemUrl = "1000-XP.png",
                TipoItem = Domain.Enums.TipoItemLoja.PacoteXp,
                QtdXp = 1000
            }
        };

        modelBuilder.Entity<ItemLoja>().HasData(itens);
    }

    public async Task<bool> Commit()
    {
        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var success = await SaveChangesAsync() > 0;
        return success;
    }
}