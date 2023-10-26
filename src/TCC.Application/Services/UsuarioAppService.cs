using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TCC.Application.Interfaces;
using TCC.Application.ViewModels;
using TCC.Domain.Interfaces;
using TCC.Domain.Models;
using TCC.Infra.Data.Context;
using TCC.Infra.Data.Repository;

namespace TCC.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private AppDbContext _appDbContext;

        public UsuarioAppService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor,
            AppDbContext context
            )
        {
            _appDbContext = context;
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
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return await _userManager
                .Users
                .Include(u => u.Pedidos)
                .ThenInclude(p => p.ItemComprado)
                .FirstOrDefaultAsync(t => t.Id.ToString() == userId);
        }

        public async Task<IdentityResult> UpdatePedidoUser(Usuario user, PedidoLoja pedido)
        {
            var result = await _userManager.UpdateAsync(user);
            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();
            return result;
        }
    }
}
