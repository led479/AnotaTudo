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
    public class FichasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FichasController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Fichas
        [HttpGet]
        public async Task<ActionResult<List<FichaDTO>>> GetFichas()
        {
            var fichas = await _context.Fichas.Include(x => x.Compras).OrderByDescending(x => x.Data).ToArrayAsync();

            return _mapper.Map<Ficha[], List<FichaDTO>>(fichas);
        }

        // GET: api/Fichas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FichaDTO>> GetFicha(int id)
        {
            var ficha = await _context.Fichas.Include(x => x.Compras).ThenInclude(x => x.Pessoa).FirstOrDefaultAsync(x => x.Id == id);

            if (ficha == null)
            {
                return NotFound();
            }

            return _mapper.Map<Ficha, FichaDTO>(ficha);
        }

        // POST: api/Fichas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FichaDTO>> PostFicha(FichaCreationDTO fichaDTO)
        {
            if (_context.Fichas.Any(x => x.Data == fichaDTO.Data))
                throw new ApplicationException("Já existe uma ficha desta data");

            var ficha = new Ficha { Data = fichaDTO.Data };

            _context.Fichas.Add(ficha);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFicha", new { id = ficha.Id }, _mapper.Map<Ficha, FichaDTO>(ficha));
        }

        // DELETE: api/Fichas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFicha(int id)
        {
            var ficha = await _context.Fichas.FindAsync(id);
            if (ficha == null)
            {
                return NotFound();
            }

            _context.Fichas.Remove(ficha);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FichaExists(int id)
        {
            return _context.Fichas.Any(e => e.Id == id);
        }
    }
}
