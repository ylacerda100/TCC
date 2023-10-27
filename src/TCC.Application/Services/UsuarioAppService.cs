using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;
using TCC.Infra.Data.Context;

namespace TCC.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioAppService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetAll()
        {
            var users = _userManager
                .Users
                .Include(u => u.Pedidos)
                .ToList();

            return _mapper.Map<IEnumerable<UsuarioViewModel>>(users);
        }

        public async Task<Usuario> GetCurrentUser()
        {
            var userId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return await _userManager
                .Users
                .Include(u => u.Pedidos)
                .ThenInclude(p => p.ItemComprado)
                .FirstOrDefaultAsync(t => t.Id.ToString() == userId);
        }

        public async Task<bool> UpdateUser(Usuario user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
