using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TCC.Application.Interfaces;
using TCC.Application.Services;
using TCC.Application.ViewModels;
using TCC.Domain.Models;

namespace TCC.UI.Web.Controllers
{
    public class LojaController : BaseController
    {
        private readonly IItemLojaAppService _lojaAppService;

        public LojaController(IItemLojaAppService lojaAppservice)
        {
            _lojaAppService = lojaAppservice;
        }

        [Authorize]
        [HttpGet("Loja")]
        public async Task<IActionResult> Index()
        {
            var itens = await _lojaAppService.GetAll();
            return View(itens
                .OrderBy(i => i.Nome)
                .ThenBy(t => t.Preco)
                );
        }

        [HttpGet("Loja/load")]
        public IActionResult Load()
        {
            if (!Debugger.IsAttached)
            {
                return NotFound();
            }

            var itens = new List<ItemLojaViewModel>
            {
                new ItemLojaViewModel
                {
                    Id = Guid.NewGuid(),
                    Nome = "Boost de XP",
                    Descricao = "Boost de XP de 75%",
                    Preco = 500,
                    ImagemUrl = "75-XP.png",
                    Duracao = TimeSpan.FromDays(3).Ticks,
                    TipoItem = Domain.Enums.TipoItemLoja.Boost,
                    Multiplicador = 0.75m
                },
                new ItemLojaViewModel
                {
                    Id = Guid.NewGuid(),
                    Nome = "Boost de XP",
                    Descricao = "Boost de XP de 50%",
                    Preco = 400,
                    ImagemUrl = "50-XP.png",
                    Duracao = TimeSpan.FromDays(3).Ticks,
                    TipoItem = Domain.Enums.TipoItemLoja.Boost,
                    Multiplicador = 0.50m
                },
                new ItemLojaViewModel
                {
                    Id = Guid.NewGuid(),
                    Nome = "Boost de XP",
                    Descricao = "Boost de XP de 25%",
                    Preco = 300,
                    ImagemUrl = "25-XP.png",
                    Duracao = TimeSpan.FromDays(3).Ticks,
                    TipoItem = Domain.Enums.TipoItemLoja.Boost,
                    Multiplicador = 0.25m
                },
                new ItemLojaViewModel
                {
                    Id = Guid.NewGuid(),
                    Nome = "Pacote de XP",
                    Descricao = "Receba 250 em XP",
                    Preco = 450,
                    ImagemUrl = "250-XP.png",
                    TipoItem = Domain.Enums.TipoItemLoja.PacoteXp,
                    QtdXp = 250
                },
                new ItemLojaViewModel
                {
                    Id = Guid.NewGuid(),
                    Nome = "Pacote de XP",
                    Descricao = "Receba 500 em XP",
                    Preco = 900,
                    ImagemUrl = "500-XP.png",
                    TipoItem = Domain.Enums.TipoItemLoja.PacoteXp,
                    QtdXp = 500
                },
                new ItemLojaViewModel
                {
                    Id = Guid.NewGuid(),
                    Nome = "Pacote de XP",
                    Descricao = "Receba 1000 em XP",
                    Preco = 1800,
                    ImagemUrl = "1000-XP.png",
                    TipoItem = Domain.Enums.TipoItemLoja.PacoteXp,
                    QtdXp = 1000
                }
            };

            foreach (var item in itens)
            {
                _lojaAppService.Add(item);
                Thread.Sleep(100);
            }

            return Ok("Itens da loja adicionados!");
        }

        [HttpGet("Loja/deleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            if (!Debugger.IsAttached)
            {
                return NotFound();
            }

            var itens = await _lojaAppService.GetAll();

            foreach (var item in itens)
            {
                await _lojaAppService.Remove(item);
                Thread.Sleep(100);
            }

            return Ok($"{itens.Count()} itens deletados!");
        }


        [HttpGet("Loja/Comprar/{id}")]
        public async Task<IActionResult> Comprar(string id)
        {
            Guid.TryParse(id, out var guidResult);
            var result = await _lojaAppService.ComprarItem(guidResult);

            return Json(result);
        }
    }
}
