using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;

namespace TCC.UI.Web.Areas.Identity.Pages.Account.Manage
{
    public class PedidosModel : PageModel
    {
        public IEnumerable<PedidoLojaViewModel> Pedidos;


        private readonly IPedidoAppService _pedidoAppService;
        private readonly IUsuarioAppService _userAppService;
        private readonly IMapper _mapper;

        public PedidosModel(
            IPedidoAppService pedidoAppService,
            IUsuarioAppService userAppService,
            IMapper mapper
            )
        {
            _pedidoAppService = pedidoAppService;
            _userAppService = userAppService;
            _mapper = mapper;
        }

        public async void OnGet()
        {
            var user = _mapper.Map<UsuarioViewModel>(await _userAppService.GetCurrentUser());

            Pedidos = await _pedidoAppService.GetAllPedidosFromUser(user);
        }
    }
}
