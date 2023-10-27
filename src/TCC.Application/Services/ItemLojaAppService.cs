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
    private readonly IPedidoAppService _pedidoAppService;


    public ItemLojaAppService(
        IItemLojaRepository itemLojaRepository,
        IMapper mapper,
        IUsuarioAppService userAppService,
        IPedidoLojaRepository pedidoRepository,
        IPedidoAppService pedidoAppService
    )
    {
        _userAppService = userAppService;
        _pedidoAppService = pedidoAppService;
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
        var errorTitle = "Não foi possível efetuar a compra.";

        if (item is null)
        {
            result = new OperationResultViewModel(
                "Item não encontrado.",
                errorTitle
                );
            return result;
        }

        if (user.QtdMoedas < item.Preco)
        {
            result = new OperationResultViewModel(
                "Você não possui moedas suficiente.",
                errorTitle
                );
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
                result = new OperationResultViewModel(
                    "Você já possui um boost ativo.",
                    errorTitle
                    );
                return result;
            }
        }

        var newPedido = new PedidoLojaViewModel()
        {
            Id = Guid.NewGuid(),
            Timestamp = DateTime.Now,
            UsuarioId = user.Id,
            ItemCompradoId = item.Id
        };

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

        var updateUser = await _userAppService.UpdateUser(user);

        var ok = await _pedidoAppService.AddPedido(newPedido, user);

        result = new OperationResultViewModel<int>(user.QtdMoedas, ok);

        return result;
    }
}