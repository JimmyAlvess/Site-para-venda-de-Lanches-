using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace LanchesMac.Repositories
{
    public class LachesRepository : ILanchesRepository
    {
        private readonly AppDbContext _context;

        public LachesRepository(AppDbContext contexto)
        {
            _context = contexto;
        }
        public IEnumerable<Lanche> Lanches => _context.Lanches;

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches
            .Where(p => p.IsLanchePreferido)
            .Include(c => c.categoria);
        public Lanche GetLancheById(int lancheid)
        {
          return  _context.Lanches.FirstOrDefault(l => l.LancheId == lancheid);
        }
    }
}
