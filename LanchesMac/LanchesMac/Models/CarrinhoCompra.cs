﻿using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;


namespace LanchesMac.Models
{
    public  class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        // injeta o contexto no construtor 
        public CarrinhoCompra(AppDbContext contexto)
        {
            _context = contexto;
        }
        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho (IServiceProvider services)
        {
            //define uma sessão
            ISession session = 
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            //obtem ou gera um id do carrinho
            string carrinhoId = session.GetString("Carrinhoid")?? Guid.NewGuid().ToString();

            //atribui o id do carrinho na Sessão
            session.SetString("Carrinhoid", carrinhoId);

            //retorna o carrinho com o contexto e o id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem =
                _context.CarrinhoCompraItens.SingleOrDefault(s => s.lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            if(carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem =
                _context.CarrinhoCompraItens.SingleOrDefault(s => s.lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if(carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }
        
        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItems ?? (CarrinhoCompraItems =
                                          _context.CarrinhoCompraItens
                                          .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                                         .Include(s => s.lanche)
                                            .ToList());           
        }
        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                                .Where(carrinho =>
                                carrinho.CarrinhoCompraId == CarrinhoCompraId);
            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }
        public decimal GetCarrinhoCompraTotal()
        {

            var total = _context.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Select(c => c.lanche.Preco * c.Quantidade).Sum();

            return (decimal)total;
        }
      
    }
}
