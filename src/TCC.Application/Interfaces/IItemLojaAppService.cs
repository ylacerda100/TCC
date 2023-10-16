using TCC.Application.ViewModels;

namespace TCC.Application.Interfaces;

public interface IItemLojaAppService : IDisposable
{
    Task<IEnumerable<ItemLojaViewModel>> GetAll();
    void Add(ItemLojaViewModel item);
    Task<bool> Remove(ItemLojaViewModel item);
    Task<OperationResultViewModel> ComprarItem(Guid id);
}