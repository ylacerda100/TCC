using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Enums;
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

            if (string.IsNullOrWhiteSpace(model.Resposta))
            {
                return Json(new { success = false, errorMessage = "Selecione uma das alternativas." });
            }

            var resposta = new RespostaAlunoExercicio
            {
                UsuarioId = user.Id,
                ExercicioId = exId,
                Resposta = model.Resposta,
                ProgressoAulaId = progresso.Id
            };

            await _respostaAppService.Add(resposta);

            if (exercicio.Resposta == model.Resposta)
            {
                var xp = exercicio.Xp * user.MultiplicadorXp;

                user.QtdMoedas += exercicio.QtdMoedas;
                user.Xp += (long)xp;

                await _userAppService.UpdateUser(user);

                return Json(new { success = true });
            }

            return Json(new { success = false, respostaCerta = exercicio.Resposta });
        }

        [HttpGet("aulas/concluir-aula/{aulaId:guid}")]
        public async Task<IActionResult> ConcluirAula(Guid aulaId)
        {
            //obter progresso
            var user = await _userAppService.GetCurrentUser();
            var progresso = await _progressoAppService.GetByAulaIdAndUserId(aulaId, user.Id);

            //validar aula já concluída
            if (progresso.Status == StatusProgresso.Concluido)
            {
                return Json(new { success = false, errorMessage = "Esta aula já foi concluída." });
            }


            //validar exercicios respondidos
            var allExerciciosCount = progresso.Aula.Exercicios.Count();
            
            if (progresso.RespostasExercicios.Count != allExerciciosCount)
            {
                return Json(new { success = false, errorMessage = "Alguns exercícios estão sem resposta." });
            }

            //atualizar progresso
            _progressoAppService.ConcluirProgresso(progresso.Id);

            if (await _progressoAppService.IsCursoConcluido(progresso.CursoId, user.Id))
            {
                return Json(new { success = true, isCursoConcluido = true, redirectUrl = $"Cursos/{progresso.CursoId}" });
            }

            var nextAula = progresso.Curso.Aulas.ToList().Find(a => a.Number == progresso.Aula.Number + 1);

            //adicionar progresso next aula
            var newProgresso = new ProgressoAula
            {
                UsuarioId = user.Id,
                Status = StatusProgresso.EmAndamento,
                AulaId = nextAula.Id,
                CursoId = progresso.CursoId
            };

            await _progressoAppService.Add(newProgresso);

            return Json(new { success = true, nextAulaId = nextAula.Id });
        }
    }
}
