using AutoMapper;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;

namespace TCC.Application.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoLojaRepository _pedidoRepo;
        private readonly IMapper _mapper;

        public PedidoAppService(
            IPedidoLojaRepository pedidoRepo,
            IMapper mapper
            )
        {
            _pedidoRepo = pedidoRepo;
            _mapper = mapper;
        }

        public async Task<bool> AddPedido(PedidoLojaViewModel pedido, Usuario user)
        {
            var domainPedido = _mapper.Map<PedidoLoja>(pedido);

            domainPedido.UsuarioId = user.Id;
            domainPedido.Usuario = user;

            return await _pedidoRepo.Add(domainPedido);
        }

        public async Task<IEnumerable<PedidoLojaViewModel>> GetAllPedidosFromUser(UsuarioViewModel user)
        {
            var domainUser = _mapper.Map<Usuario>(user);
            return _mapper.Map<IEnumerable<PedidoLojaViewModel>>(await _pedidoRepo.GetAllFromUser(domainUser));
        }
    }
}
