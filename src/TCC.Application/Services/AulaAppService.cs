using AutoMapper;
using DtronixPdf;
using DtronixPdf.ImageSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Interfaces;

namespace TCC.Application.Services
{
    public class AulaAppService : IAulaAppService
    {
        private readonly IAulaRepository _aulaRepository;
        private readonly IMapper _mapper;

        public AulaAppService(
            IAulaRepository aulaRepository,
            IMapper mapper
            )
        {
            _aulaRepository = aulaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<byte[]>> ConvertPdfToImages(string filePath)
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
                return images;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<AulaViewModel> GetById(Guid id)
        {
            return _mapper.Map<AulaViewModel>(await _aulaRepository.GetById(id));
        }

        public async Task<AulaViewModel> GetByName(string name)
        {
            return _mapper.Map<AulaViewModel>(await _aulaRepository.GetByName(name));
        }
    }
}
