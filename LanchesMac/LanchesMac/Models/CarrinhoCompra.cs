using LanchesMac.Context;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
using Microsoft.EntityFrameworkCore.Query.Internal;
>>>>>>> 1d14294890b806d023d5d680f0ffa04f31dc3631

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;
<<<<<<< HEAD

        // injeta o contexto no construtor 
        public CarrinhoCompra(AppDbContext contexto)
        {
            _context = contexto;
        }
        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public static CarrinhoCompra GetCarrinho (IServiceProvider services)
        {
            //define uma sessão
            ISession session = 
=======
        public CarrinhoCompra(AppDbContext contex)
        {
            _context = contex;
        }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public string CarrinhoCompraId { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define uma sessão
            ISession session =
>>>>>>> 1d14294890b806d023d5d680f0ffa04f31dc3631
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //obtem um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

<<<<<<< HEAD
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
                    _context.CarrinhoCompraItems.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }
        
        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItems ?? (CarrinhoCompraItems ==
                                          _context.CarrinhoCompraItens
                                          .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                                          .Include(s => s.lanche)
                                          .ToList());
        }
      
=======
            //obtem ou gera o id do carrinho
            string carrinhoid = session.GetString("CarrinhoId")?? Guid.NewGuid().ToString();

            //atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoid);

            //retorna o carrinho com o carrinho com o contexto e o id atribuído ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoid
            };
        }

>>>>>>> 1d14294890b806d023d5d680f0ffa04f31dc3631
    }
}
