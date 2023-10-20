using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfiumViewer;
using System.Drawing;
using System.Drawing.Imaging;
using TCC.Application.Interfaces;

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

            using (var pdfDocument = PdfDocument.Load(filePath))
            {
                var images = new List<byte[]>();

                var bitmapImage = pdfDocument.Render(0, 700, 700, true);

                ImageConverter converter = new ImageConverter();
                var imgBytes = (byte[])converter.ConvertTo(bitmapImage, typeof(byte[]));

                images.Add(imgBytes);
                ViewBag.Images = images;
            }

            return View(aulaViewModel);
        }
    }
}
