using APIVenda.Net.DTOs;
using APIVenda.Net.DTOs.Validations;
using APIVenda.Net.Entities;
using APIVenda.Net.Enum;
using APIVenda.Net.Interfaces;
using APIVenda.Net.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Data;

namespace APIVenda.Net.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;


        public VendaService(IVendaRepository vendaRepository, IMapper mapper, IProdutoRepository produtoRepository, IVendedorRepository vendedorRepository)
        {
            _vendaRepository = vendaRepository;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _vendedorRepository = vendedorRepository;
        }

        public async Task<ResultService<VendaDto>> GetByIdAsync(Guid id)
        {
            var venda = await _vendaRepository.GetByIdAsync(id);
            if (venda == null)
                return ResultService.Fail<VendaDto>("A venda não foi localizada");

            return ResultService.Ok<VendaDto>(_mapper.Map<VendaDto>(venda));
        }

        public async Task<ResultService<ICollection<VendaResponseDto>>> GetAllAsync()
        {
            var vendas = await _vendaRepository.GetAllAsync();
            if (vendas == null)
                return ResultService.Fail<ICollection<VendaResponseDto>>("A venda não foi localizada");

            return ResultService.Ok<ICollection<VendaResponseDto>>(_mapper.Map<ICollection<VendaResponseDto>>(vendas));
        }

        public async Task<ResultService<VendaDto>> ObterStatus(StatusVenda statusVenda)
        {
            var status = await _vendaRepository.ObterStatus(statusVenda);
            if (status == null)
                return ResultService.Fail<VendaDto>("A venda não foi localizada");

            return ResultService.Ok<VendaDto>(_mapper.Map<VendaDto>(status));
        }

        public async Task<ResultService<VendaDto>> CreateAsync(VendaDto vendaDto)
        {
            
            if (vendaDto == null)
                return ResultService.Fail<VendaDto>("Necessário preencher todos os campos!");

            var resultado = new VendaDtoValidator().Validate(vendaDto);
            if (!resultado.IsValid)
                return ResultService.RequestError<VendaDto>("Problemas na validação", resultado);


            var produto = await _produtoRepository.GetByIdAsync(vendaDto.ProdutoId);

            var vendedor = await _vendedorRepository.GetByIdAsync(vendaDto.VendedorId);


            var novaVenda = new Venda(DateTime.Now,vendedor, produto, vendaDto.StatusVenda); 
            
            var dadosSalvos = await _vendaRepository.CreateAsync(novaVenda);
            return ResultService.Ok<VendaDto>(_mapper.Map<VendaDto>(dadosSalvos));
        }

        public async Task<ResultService> UpdateAsync(VendaDto vendaDto)
        {
            if (vendaDto == null)
                return ResultService.Fail<VendaDto>("Necessário preencher dados solicitados!");

            var resultado = new VendaDtoValidator().Validate(vendaDto);
            if (!resultado.IsValid)
                return ResultService.RequestError<VendaDto>("Problemas na validação", resultado);

            var venda = await _vendaRepository.GetByIdAsync(vendaDto.Id);
            if (venda == null)
                return ResultService.Fail("Venda não encontrado!");

            venda = _mapper.Map(vendaDto, venda);
            await _vendaRepository.EditAsync(venda);
            return ResultService.Ok("Venda Editada com sucesso!");
        }


        public async Task<ResultService> UpdateStatusVendaAsync(Guid id, StatusVenda status)
        {
            var venda = await _vendaRepository.GetByIdAsync(id);
            if (venda == null)
                return ResultService.Fail($"Venda com o id {id} não foi encontrada!");

            switch (status)
            {
                case (StatusVenda)1:
                    venda.SetStatusVenda(StatusVenda.AGUARDANDO_PAGAMENTO);
                    break;
                case (StatusVenda)2:
                    venda.SetStatusVenda(StatusVenda.PAGAMENTO_APROVADO);
                    break;
                case (StatusVenda)3:
                    venda.SetStatusVenda(StatusVenda.ENVIADO_PARA_TRANSPORTADORA);
                    break;
                case (StatusVenda)4:
                    venda.SetStatusVenda(StatusVenda.CANCELADA);
                    break;
                case (StatusVenda)5:
                    venda.SetStatusVenda(StatusVenda.ENTREGUE);
                    break;
                default:
                    return ResultService.Fail(" Status de venda inválido ou nulo. Passe:" +
                                              " '1' para 'Aguardando Pagamento'," +
                                              " '2' para 'Pagamento Aprovado'," +
                                              " '3' para 'ENVIADO_PARA_TRANSPORTADORA' ," +
                                              " '4' para 'CANCELADA'," +
                                              " '5' para 'ENTREGUE',");
            }
            await _vendaRepository.EditAsync(venda);
            return ResultService.Ok($"Status da venda com o id {id} foi editado com sucesso!");
        }
        public async Task<ResultService> RemoveAsync(Guid id)
        {
            var venda = await _vendaRepository.GetByIdAsync(id);
            if (venda == null)
                return ResultService.Fail("Venda não encontrada!");

            await _vendaRepository.DeleteAsync(venda);
            return ResultService.Ok($"Venda com o id {id} foi deletada com sucesso!");
        }
    }
}
