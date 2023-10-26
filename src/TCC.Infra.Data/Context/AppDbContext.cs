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
        #endregion


        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PedidoLoja>()
           .HasOne(pedido => pedido.ItemLoja)
           .WithMany(item => item.Pedidos)
           .HasForeignKey(pedido => pedido.ItemLojaId);

        modelBuilder.Entity<PedidoLoja>()
            .HasOne(pedido => pedido.Usuario)
            .WithMany(usuario => usuario.Pedidos)
            .HasForeignKey(pedido => pedido.UsuarioId);

        #region Usuario map
        modelBuilder.Entity<Usuario>()
                .Property(e => e.Nome)
        .HasMaxLength(250);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Pedidos);

        modelBuilder.Entity<Usuario>()
            .Navigation(e => e.Pedidos)
            .AutoInclude();
        #endregion
    }

    public async Task<bool> Commit()
    {
        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var success = await SaveChangesAsync() > 0;
        return success;
    }
}