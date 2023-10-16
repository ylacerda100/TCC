using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;
using TCC.Infra.Data.Context;

namespace TCC.Infra.Data.Repository;

public class ItemLojaRepository : IItemLojaRepository
{
    protected readonly AppDbContext Db;
    protected readonly DbSet<ItemLoja> DbSet;
    public IUnitOfWork UnitOfWork => Db;
    
    public ItemLojaRepository(AppDbContext context)
    {
        Db = context;
        DbSet = Db.Set<ItemLoja>();
    }
    
    public async Task<ItemLoja> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<ItemLoja>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<bool> Add(ItemLoja item)
    {
        DbSet.Add(item);
        return await Db.Commit();
    }

    public void Update(ItemLoja item)
    {
        DbSet.Update(item);
    }

    public async Task<bool> Remove(ItemLoja item)
    {
        DbSet.Remove(item);
        return await Db.Commit();
    }
    
    public void Dispose()
    {
        Db.Dispose();
    }
}