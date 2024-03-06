using BookSamsys.DAL.Repositories.Context;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BookSamsys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase {

        private readonly LivroContext _context;

        public LivroController(LivroContext context) {
            _context = context;
        }

        //Obter todos os livros
        //async - segundo plano
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var livros = await _context.Livros.ToListAsync();

            List<LivroDTO> obterLivros = new List<LivroDTO>();

            foreach(var livro in livros) {
                obterLivros.Add(new LivroDTO { Isbn = livro.Isbn, Nome = livro.Nome, Autor = livro.Autor, Preco = livro.Preco});
            }

            return obterLivros.Any()
                ? Ok(obterLivros)
                : BadRequest("Livro não encontrado");
        }

        //Obter um livro
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var livro = await _context.Livros.FindAsync(id);

            var obterLivro = new DetalheLivroDTO { Isbn = livro.Isbn, Nome = livro.Nome, Autor = livro.Autor, Preco = livro.Preco };

            return obterLivro == null ? NotFound("Livro não encontrado") : Ok(obterLivro); 
        }
        /*
        //Criar um livro
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Livro>>> AddLivro(Livro livro) {
            var livro = await _context.Livros.AddAsync(livro);
            if (livro == null) return NotFound("Livro não encontrado");

            return Ok(livro);
        }*/

    }
}
