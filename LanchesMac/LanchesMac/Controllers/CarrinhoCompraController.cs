using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILanchesRepository _lanchesRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        public CarrinhoCompraController(ILanchesRepository lanchesRepository, CarrinhoCompra carrinhoCompra)
        {
            _lanchesRepository = lanchesRepository;
            _carrinhoCompra = carrinhoCompra;
        }
        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();

            _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoTotal()
            };

            return View(carrinhoCompraVM);
        }
        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheid)
        {
            var lancheSelecionado = _lanchesRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheid);
            
            if(lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoverItemDoCarrinho (int lancheId)
        {
            var lancheSelecionado = _lanchesRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheId);

            if(lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }
    }
}

    

