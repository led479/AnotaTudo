using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AnotaTudo;

namespace AnotaTudo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ComprasController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Compras?notaId=5
        [HttpGet]
        public async Task<ActionResult<List<CompraDTO>>> GetCompras(int fichaId)
        {
            var ficha = await _context.Fichas.Include(x => x.Compras).ThenInclude(x => x.Pessoa).FirstOrDefaultAsync(x => x.Id == fichaId);

            if (ficha is null)
                throw new ApplicationException($"Ficha com id '{fichaId}' não foi encontrada");

            return _mapper.Map<List<Compra>, List<CompraDTO>>(ficha.Compras);
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompraDTO>> GetCompra(int id)
        {
            var compra = await _context.Compras.FindAsync(id);

            if (compra == null)
            {
                return NotFound();
            }

            return _mapper.Map<Compra, CompraDTO>(compra);
        }

        // PUT: api/Compras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(int id, CompraDTO compraDTO)
        {
            if (id != compraDTO.Id)
            {
                return BadRequest();
            }

            var ficha = await _context.Fichas.FindAsync(compraDTO.FichaId);

            if (ficha is null)
                throw new ApplicationException($"Ficha com id '{compraDTO.FichaId}' não foi encontrada");

            var pessoa = await _context.Pessoas.FindAsync(compraDTO.PessoaId);

            if (pessoa is null)
                throw new ApplicationException($"Pessoa com id '{compraDTO.PessoaId}' não foi encontrada");

            var compra = await _context.Compras.FindAsync(compraDTO.Id);

            compra.Item = compraDTO.Item;
            compra.FormaPagamento = compraDTO.FormaPagamento;
            compra.Valor = compraDTO.Valor;
            compra.Ficha = ficha;
            compra.Pessoa = pessoa;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompraDTO>> PostCompra(CompraCreationDTO compraDTO)
        {
            var ficha = await _context.Fichas.FindAsync(compraDTO.FichaId);

            if (ficha is null)
                throw new ApplicationException($"Ficha com id '{compraDTO.FichaId}' não foi encontrada");

            var pessoa = await _context.Pessoas.FindAsync(compraDTO.PessoaId);

            if (pessoa is null)
                throw new ApplicationException($"Pessoa com id '{compraDTO.PessoaId}' não foi encontrada");

            var compra = new Compra { Item = compraDTO.Item, FormaPagamento = compraDTO.FormaPagamento, Valor = compraDTO.Valor, Ficha = ficha, Pessoa = pessoa };

            _context.Compras.Add(compra);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompra", new { id = compra.Id }, _mapper.Map<Compra, CompraDTO>(compra));
        }

        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }

            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.Id == id);
        }
    }
}
