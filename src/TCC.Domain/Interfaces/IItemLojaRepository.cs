using NetDevPack.Data;
using TCC.Domain.Models;

namespace TCC.Domain.Interfaces
{
    public interface IItemLojaRepository : IRepository<ItemLoja>
    {
        Task<ItemLoja> GetById(Guid id);
        Task<IEnumerable<ItemLoja>> GetAll();
        Task<bool> Add(ItemLoja item);
        void Update(ItemLoja item);
        Task<bool> Remove(ItemLoja item);
    }
}
