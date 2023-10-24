using AutoMapper;
using System.Xml.Linq;
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
