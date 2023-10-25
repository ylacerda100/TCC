using DtronixPdf;
using DtronixPdf.ImageSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDFiumCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using System.Drawing;
using System.Drawing.Imaging;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using static System.Net.Mime.MediaTypeNames;

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
        [HttpGet("Aulas/{id:guid}")]
        public async Task<IActionResult> Detalhes(Guid? id)
        {
            var aulaViewModel = await _aulaAppService.GetById(id.Value);

            if (aulaViewModel is null)
            {
                return NotFound();
            }

            var baseDir = $"{_env.WebRootPath}/assets/pdf/TiposDeDados";

            var filePath = $"{baseDir}/TiposDeDados1.pdf";

            try
            {
                ConvertPdfToImage(filePath);
            }
            catch (Exception ex)
            {
                var files = Directory.GetFiles(baseDir);
                return BadRequest($"Erro ao tentar converter PDF. {filePath} | {ex.Message} | {ex.StackTrace} | Arquivos: {string.Join(';', files)}");
            }

            return View(aulaViewModel);
        }

        private void ConvertPdfToImage(string filePath)
        {
            var images = new List<byte[]>();
            using (var document = PdfDocument.Load(filePath, null))
            {
                for (int pageNumber = 0; pageNumber < document.Pages; pageNumber++)
                {
                    var page = document.GetPage(pageNumber);
                    var result = page.Render(2);
                    var image = result.GetImage();

                    using (var memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, PngFormat.Instance);
                        images.Add(memoryStream.ToArray());
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

                //updat e user

                return Ok(new { success = true });
            }

            return Ok(new { success = false });
        }
    }
}
