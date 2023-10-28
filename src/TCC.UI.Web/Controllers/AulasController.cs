using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;

namespace TCC.UI.Web.Controllers
{
    public class AulasController : Controller
    {
        private readonly IAulaAppService _aulaAppService;
        private readonly IExercicioAppService _exercicioAppService;
        private readonly IUsuarioAppService _userAppService;
        private readonly IProgressoAppService _progressoAppService;
        private readonly IWebHostEnvironment _env;

        public AulasController(
            IAulaAppService aulaAppService,
            IWebHostEnvironment env,
            IExercicioAppService exercicioAppService,
            IUsuarioAppService userAppService,
            IProgressoAppService progressoAppService)
        {
            _aulaAppService = aulaAppService;
            _env = env;
            _exercicioAppService = exercicioAppService;
            _userAppService = userAppService;
            _progressoAppService = progressoAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("Aulas/{id:guid}")]
        public async Task<IActionResult> Detalhes(Guid? id)
        {
            var aulaViewModel = await _aulaAppService.GetById(id.Value);

            if (aulaViewModel is null)
            {
                return NotFound();
            }

            var baseDir = $"{_env.WebRootPath}/assets/pdf";
            var filePath = $"{baseDir}/{aulaViewModel.ContentUrl}";

            ViewBag.Images = _aulaAppService.ConvertPdfToImages(filePath);

            return View(aulaViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResponderExercicio(RespostaExercicioViewModel model)
        {
            Guid.TryParse(model.ExercicioId, out var exId);
            var exercicio = await _exercicioAppService.GetById(exId);
            var user = await _userAppService.GetCurrentUser();

            if (exercicio.Resposta == model.Resposta)
            {
                var xp = exercicio.Xp * user.MultiplicadorXp;

                user.QtdMoedas += exercicio.QtdMoedas;
                user.Xp += (long)xp;

                //updat e user

                return Ok(new { success = true });
            }

            return Ok(new { success = false });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ConcluirAula(Guid? aulaId)
        {
            var aula = await _aulaAppService.GetById(aulaId.Value);
            return RedirectToAction("Detalhes", "Aulas", new { aula.Id });
        }
    }
}
