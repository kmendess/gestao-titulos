using GestaoTitulos.Domain.Entities;

namespace GestaoTitulos.Domain.Interfaces
{
    public interface ITituloRepository
    {
        Task<List<Titulo>> ObterTodos(CancellationToken cancellationToken = default);
        Task Adicionar(Titulo titulo, CancellationToken cancellationToken = default);
        Task<Titulo?> ObterPorNumero(string numeroTitulo, CancellationToken cancellationToken = default);
        Task Remover(Titulo titulo, CancellationToken cancellationToken = default);
    }
}
