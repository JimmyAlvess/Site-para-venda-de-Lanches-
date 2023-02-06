using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lanchesRepository;

        public LancheController(ILancheRepository lanchesRepository)
        {
            _lanchesRepository = lanchesRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lanchesRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                //if(string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase)) 
                //{
                //    lanches = _lanchesRepository.Lanches
                //        .Where(l => l.categoria.CategoriaNome.Equals("Normal"))
                //        .OrderBy(c => c.Nome);

                //}
                //else
                //{
                //    lanches = _lanchesRepository.Lanches
                //        .Where(l => l.categoria.CategoriaNome.Equals("Natural"))
                //        .OrderBy(c => c.Nome);
                //}
                lanches = _lanchesRepository.Lanches
                    .Where(l => l.categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(c => c.Nome);

                categoriaAtual = categoria;
            }

            var lancheListViewModel = new LancheListViewModel
            {
                lanches = lanches,
                CategoriaAtual = categoriaAtual
            };
            return View(lancheListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lanchesRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
            return View(lanche);
        }
    }
}
