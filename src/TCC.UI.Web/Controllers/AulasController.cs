using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Models;

namespace TCC.UI.Web.Controllers
{
    public class AulasController : Controller
    {
        private readonly IAulaAppService _aulaAppService;
        private readonly IExercicioAppService _exercicioAppService;
        private readonly IRespostaAlunoExercicioAppService _respostaAppService;
        private readonly IUsuarioAppService _userAppService;
        private readonly IProgressoAppService _progressoAppService;
        private readonly IWebHostEnvironment _env;

        public AulasController(
            IAulaAppService aulaAppService,
            IWebHostEnvironment env,
            IExercicioAppService exercicioAppService,
            IUsuarioAppService userAppService,
            IProgressoAppService progressoAppService,
            IRespostaAlunoExercicioAppService respostaAppService)
        {
            _aulaAppService = aulaAppService;
            _env = env;
            _exercicioAppService = exercicioAppService;
            _userAppService = userAppService;
            _progressoAppService = progressoAppService;
            _respostaAppService = respostaAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("Aulas/{id:guid}")]
        public async Task<IActionResult> Detalhes(Guid? id)
        {
            var user = await _userAppService.GetCurrentUser();
            var progressoAula = await _progressoAppService.GetByAulaIdAndUserId(id.Value, user.Id);

            var baseDir = $"{_env.WebRootPath}/assets/pdf";
            var filePath = $"{baseDir}/{progressoAula.Aula.ContentUrl}";

            ViewBag.Images = await _aulaAppService.ConvertPdfToImages(filePath);

            return View(progressoAula);
        }

        [HttpPost]
        public async Task<IActionResult> ResponderExercicio(RespostaExercicioViewModel model)
        {
            Guid.TryParse(model.ExercicioId, out var exId);
            Guid.TryParse(model.AulaId, out var aulaId);

            var exercicio = await _exercicioAppService.GetById(exId);
            var user = await _userAppService.GetCurrentUser();

            var progresso = await _progressoAppService.GetByAulaIdAndUserId(aulaId, user.Id);

            if (progresso is null)
            {
                return BadRequest("O Curso não foi iniciado");
            }

            var resposta = new RespostaAlunoExercicio
            {
                UsuarioId = user.Id,
                ExercicioId = exId,
                Resposta = model.Resposta
            };

            await _respostaAppService.Add(resposta);

            if (exercicio.Resposta == model.Resposta)
            {
                var xp = exercicio.Xp * user.MultiplicadorXp;

                user.QtdMoedas += exercicio.QtdMoedas;
                user.Xp += (long)xp;

                _userAppService.UpdateUser(user);

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
