using AutoMapper;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Enums;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;

namespace TCC.Application.Services;

public class ItemLojaAppService : IItemLojaAppService
{
    private readonly IItemLojaRepository _itemLojaRepository;
    private readonly IMapper _mapper;
    private readonly IUsuarioAppService _userAppService;

    public ItemLojaAppService(
        IItemLojaRepository itemLojaRepository,
        IMapper mapper,
        IUsuarioAppService userAppService,
        IPedidoLojaRepository pedidoRepository
    )
    {
        _userAppService = userAppService;
        _itemLojaRepository = itemLojaRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ItemLojaViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<ItemLojaViewModel>>(await _itemLojaRepository.GetAll());
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async void Add(ItemLojaViewModel item)
    {
        await _itemLojaRepository.Add(_mapper.Map<ItemLoja>(item));
    }

    public async Task<bool> Remove(ItemLojaViewModel item)
    {
        var itemDomain = _mapper.Map<ItemLoja>(item);

        return await _itemLojaRepository.Remove(itemDomain);
    }

    public async Task<OperationResultViewModel> ComprarItem(Guid id)
    {
        var result = new OperationResultViewModel();

        var user = await _userAppService.GetCurrentUser();
        var item = await _itemLojaRepository.GetById(id);
        
        if (item is null)
        {
            result = new OperationResultViewModel("Item não encontrado.");
            return result;
        }

        if (user.QtdMoedas < item.Preco)
        {
            result = new OperationResultViewModel("Você não possui moedas suficiente.");
            return result;
        }

        if (user.Pedidos != null)
        {
            var pedidoUser = user.Pedidos.FirstOrDefault(p => p.ItemComprado.Id == id);

            if (pedidoUser != null &&
                pedidoUser.ItemComprado.TipoItem == TipoItemLoja.Boost &&
                !pedidoUser.IsExpired()
                )
            {
                result = new OperationResultViewModel("Você já possui um boost ativo.");
                return result;
            }
        }

        if (user.Pedidos is null)
        {
            user.Pedidos = new List<PedidoLoja>();
        }

        var newPedido = new PedidoLoja()
        {
            Id = Guid.NewGuid(),
            Timestamp = DateTime.Now,
            ItemComprado = item
        };

        user.Pedidos.Add(newPedido);
        user.QtdMoedas -= item.Preco;

        switch (item.TipoItem)
        {
            case TipoItemLoja.Boost:
                user.MultiplicadorXp += item.Multiplicador;
                break;
            case TipoItemLoja.PacoteXp:
                user.Xp += item.QtdXp;
                break;
        }

        await _userAppService.UpdatePedidoUser(user, newPedido);
        return result;
    }
}