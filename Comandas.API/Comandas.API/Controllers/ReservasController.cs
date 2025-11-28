using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comandas.API;
using Comandas.API.Models; 

namespace Comandas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly ComandasDbContext _context; 

        public ReservasController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.Reservas.ToListAsync();
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        // PUT: api/Reservas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }
            // ATUALIZAÇÃO 
            _context.Entry(reserva).State = EntityState.Modified;

            // REMOÇÃO E A INCLUSÃO DA RESERVA NA MESA (2 - RESERVADA - 1 LIVRE)
            // 2 - LIVRE 1 - RESERVADA
            //MUDAR A SITUAÇÃO PARA RESERVADA DA NOVA MESA 
            var novaMesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            if (novaMesa is null)
                return BadRequest("Mesa não encontrada");
            novaMesa.SituacaoMesa = (int)SituacaoMesa.Reservada;

            //CONSULTA DADOS DA RESERVA ORIGINAL 
            var reservaOriginal = await _context.Reservas.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            //CONSULTA NUMERO MESA ORIGINAL           
            var numeroMesaOriginal = reservaOriginal!.NumeroMesa;
            //CONSULTA A MESA ORIGINAL 
            var mesaOrignal = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == numeroMesaOriginal);
            mesaOrignal!.SituacaoMesa = (int)SituacaoMesa.Livre; // MESA ORIGINAL AGORA ESTÁ LIVRE

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reservas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            _context.Reservas.Add(reserva);

            //atualizar o status da mesa para "Reservada"
            //CONSULTANDO A MESA PELO NUMERO
            var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            if (mesa is null)
            {
                return BadRequest("Mesa não encontrada.");
            }
            //SE MESA FOI ENCONTRADA
            if (mesa is not null) 
            {
                if (mesa.SituacaoMesa != (int)SituacaoMesa.Livre)
                {
                    return BadRequest("Mesa não está disponível para reserva.");
                }
                mesa.SituacaoMesa = (int)SituacaoMesa.Reservada;
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserva", new { id = reserva.Id }, reserva);
        }

        // DELETE: api/Reservas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound("Reserva não encontrada");
            }
            //CONSULTAR A MESA
            var mesa = await _context.Mesas.FirstOrDefaultAsync(m => m.NumeroMesa == reserva.NumeroMesa);
            if( mesa is null) 
            {
                return BadRequest("Mesa não encontrada.");
            }
            //ATUALIZAR A MESA PARA LIVRE
            mesa.SituacaoMesa = (int)SituacaoMesa.Livre; //(int) converte o enum para int
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
