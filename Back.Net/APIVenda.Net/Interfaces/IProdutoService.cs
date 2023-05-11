using APIVenda.Net.DTOs;
using APIVenda.Net.Services;

namespace APIVenda.Net.Interfaces;

public interface IProdutoService
{
    Task<ResultService<ICollection<ProdutoDto>>> GetAllAsync();
    Task<ResultService<ProdutoDto>> GetByIdAsync(Guid id);
    Task<ResultService<ProdutoDto>> CreateAsync(ProdutoCadastroDto produtocadastroDto);
    Task<ResultService> UpdateAsync(ProdutoDto produtoDto);
    Task<ResultService> RemoveAsync(Guid id);


}
