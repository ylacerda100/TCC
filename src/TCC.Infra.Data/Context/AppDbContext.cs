using Microsoft.AspNetCore.Identity;
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

        #endregion

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var success = await SaveChangesAsync() > 0;
        return success;
    }
}