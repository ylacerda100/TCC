using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfiumViewer;
using System.Drawing;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;

namespace TCC.UI.Web.Controllers
{
    public class AulasController : Controller
    {
        private readonly IAulaAppService _aulaAppService;
        private readonly IExercicioAppService _exercicioAppService;
        private readonly IUsuarioAppService _userAppService;
        private readonly IWebHostEnvironment _env;

        public AulasController(
            IAulaAppService aulaAppService, 
            IWebHostEnvironment env,
            IExercicioAppService exercicioAppService,
            IUsuarioAppService userAppService)
        {
            _aulaAppService = aulaAppService;
            _env = env;
            _exercicioAppService = exercicioAppService;
            _userAppService = userAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("Aulas/{name}")]
        public async Task<IActionResult> Detalhes(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            var aulaViewModel = await _aulaAppService.GetByName(name);

            if (aulaViewModel is null)
            {
                return NotFound();
            }

            ConvertPdfToImage(aulaViewModel);

            return View(aulaViewModel);
        }

        private void ConvertPdfToImage(AulaViewModel aulaViewModel)
        {
            var filePath = Path.Combine(
                _env.ContentRootPath, "wwwroot", "assets", "pdf", aulaViewModel.ContentUrl);

            using (var document = PdfDocument.Load(filePath))
            {
                var images = new List<byte[]>();
                var dpi = 300;

                for (int pageNumber = 0; pageNumber < document.PageCount; pageNumber++)
                {
                    SizeF sizeInPoints = document.PageSizes[pageNumber];
                    int widthInPixels = (int)Math.Round(sizeInPoints.Width * (float)dpi / 72F);
                    int heightInPixels = (int)Math.Round(sizeInPoints.Height * (float)dpi / 72F);

                    using (Image image = document.Render(pageNumber, widthInPixels, heightInPixels, dpi, dpi, true))
                    {
                        ImageConverter converter = new ImageConverter();
                        var imgBytes = (byte[])converter.ConvertTo(image, typeof(byte[]));
                        images.Add(imgBytes);
                    }
                }
                ViewBag.Images = images;
            }
        }

        [HttpPost]
        public async Task<ActionResult> ResponderExercicio(RespostaExercicioViewModel model)
        {
            Guid.TryParse(model.ExercicioId, out var exId);
            var exercicio = await _exercicioAppService.GetById(exId);
            var user = await _userAppService.GetCurrentUser();

            if (exercicio.Resposta == model.Resposta)
            {
                var xp = exercicio.Xp * user.MultiplicadorXp;

                user.QtdMoedas += exercicio.QtdMoedas;
                user.Xp += (long)xp;

                //update user


                return Ok(new { success = true });
            }

            return Ok(new { success = false });

        }
    }
}
