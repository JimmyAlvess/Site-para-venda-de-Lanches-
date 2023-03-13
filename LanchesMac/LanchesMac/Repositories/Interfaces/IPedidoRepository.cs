using LanchesMac.Models;
using Newtonsoft.Json.Bson;

namespace LanchesMac.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
        //void BuscarPedido(Pedido pedido);
     

        
    }
}
