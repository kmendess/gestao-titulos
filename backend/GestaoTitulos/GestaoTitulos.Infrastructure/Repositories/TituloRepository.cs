using GestaoTitulos.Domain.Entities;
using GestaoTitulos.Domain.Interfaces;
using GestaoTitulos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestaoTitulos.Infrastructure.Repositories
{
    public class TituloRepository : ITituloRepository
    {
        private readonly AppDbContext _context;

        public TituloRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Titulo>> ObterTodos(CancellationToken cancellationToken = default)
        {
            return await _context.Titulos
                .AsNoTracking()
                .Include(t => t.Parcelas)
                .OrderByDescending(t => t.CriadoEm)
                .ToListAsync(cancellationToken);
        }

        public async Task Adicionar(Titulo titulo, CancellationToken cancellationToken = default)
        {
            await _context.Titulos.AddAsync(titulo, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Titulo?> ObterPorNumero(string numeroTitulo, CancellationToken cancellationToken = default)
        {
            return await _context.Titulos
                .Include(t => t.Parcelas)
                .FirstOrDefaultAsync(t => t.NumeroTitulo == numeroTitulo, cancellationToken);
        }

        public async Task Remover(Titulo titulo, CancellationToken cancellationToken)
        {
            _context.Titulos.Remove(titulo);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
