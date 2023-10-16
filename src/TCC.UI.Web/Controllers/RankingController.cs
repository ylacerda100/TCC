using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Interfaces;

namespace TCC.UI.Web.Controllers
{
    public class RankingController : BaseController
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public RankingController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var users = await _usuarioAppService.GetAll();
            return View(users.OrderByDescending(u => u.Xp));
        }
    }
}
