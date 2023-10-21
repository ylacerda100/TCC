using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PdfiumViewer;
using System.Drawing;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;

namespace TCC.UI.Web.Controllers
{
    public class AulasController : Controller
    {
        private readonly IAulaAppService _aulaAppService;
        private readonly IWebHostEnvironment _env;

        public AulasController(IAulaAppService aulaAppService, IWebHostEnvironment env)
        {
            _aulaAppService = aulaAppService;
            _env = env;
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

            var filePath = Path.Combine(
                _env.ContentRootPath, "wwwroot", "assets", "pdf", aulaViewModel.ContentUrl);

            using (var document = PdfDocument.Load(filePath)) // C# Read PDF Document
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

            return View(aulaViewModel);
        }

        [HttpPost]
        public ActionResult ResponderExercicio(RespostaExercicioViewModel model)
        {
            return NotFound(new { message = "Deu certo", errorMessage="Teste" });
        }
    }
}
