using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly DataContext _context;
        public EventosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get")]

        public IEnumerable<Evento> Get()
        {
            return _context.Eventos;
        }
        
        [HttpGet("{id}")]
        public Evento ObterPorId(int id)
        {
            return _context.Eventos.FirstOrDefault(x => x.EventoId == id);
        }
        [HttpPost("Criar")]
        public IActionResult Criar(Evento evento)
        {
            _context.Add(evento);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = evento.EventoId }, evento);
        }
        [HttpPut("Atualizar{id}")]
        public IActionResult Atualizar(int id, Evento evento)
        {
            var eventoBanco = _context.Eventos.Find(id);

            if (eventoBanco == null)
                return NotFound();

            eventoBanco.Tema = evento.Tema;
            eventoBanco.Local = evento.Local;
            eventoBanco.DataEvento = evento.DataEvento;
            eventoBanco.QtdPessoas = evento.QtdPessoas;
            eventoBanco.Lote = evento.Lote;

            _context.Eventos.Update(evento);
            _context.SaveChanges();
                return Ok (evento);
        }
        [HttpDelete]
        public IActionResult Deletar(int id, Evento evento)
        {
            _context.Eventos.Remove(evento);
            _context.SaveChanges();
            return Ok(evento);
        }
    }
}