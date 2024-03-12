using Azure;
using BookSamsys.BLL.Services;
using BookSamsys.DAL.Context;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;

namespace BookSamsys.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase {

        private readonly IBookService _bookService;

        public BookController(IBookService bookService) {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<MessagingHelper<IEnumerable<BookDTO>>> GetAll() {
            var responseBooksDTO = await _bookService.GetAll();
            return responseBooksDTO;
        }

        [HttpGet("{id}")]
        public async Task<MessagingHelper<BookDTO>> GetById(int id) {
            var responseBookDTO = await _bookService.GetById(id);
            return responseBookDTO;
        }

        [HttpPost]
        public async Task<MessagingHelper<BookPostDTO>> Create(BookPostDTO bookPostDTO) {
            //var responseAvailable = await _bookService.AvailabilityIsbn(bookPostDTO.Isbn);
            //var responseValid = await _bookService.ValidatePrice(bookPostDTO.Preco);
            var responseBookDTOCreated = await _bookService.Create(bookPostDTO);
            return responseBookDTOCreated;
            /*if (responseBookDTOCreated.Success == false) {
                return BadRequest();
            } else {
                return Ok();
            }*/
        }
        /*
        [HttpPut]
        public async Task<ActionResult> Update(BookDTO bookDTO) {
            var available = await _bookService.AvailabilityIsbn(bookDTO.Isbn);
            var valid = await _bookService.ValidatePrice(bookDTO.Preco);

            if (!available) {
                return BadRequest("O Isbn não pode ser repetido.");
            }
            if (!valid) {
                return BadRequest("O preço não pode ser negativo.");
            }

            var bookDTOUpdated = await _bookService.Update(bookDTO);
            return bookDTOUpdated == null ? BadRequest("Ocorreu um erro ao alterar o livro.") : Ok("Livro alterado com sucesso!");
        }*/

        [HttpDelete]
        public async Task<MessagingHelper<BookDTO>> Delete(int id) {
            var responseBookDTODeleted = await _bookService.Delete(id);
            return responseBookDTODeleted;
        }
    }

    /*[HttpGet]
    public async Task<ActionResult> GetAll() {
        var booksDTO = await _bookService.GetAll(response);
        return Ok(booksDTO);
    }

    [HttpGet("{id}")]
    public async Task<MessagingHelper<BookDTO>> GetById(int id) {
        var responseBookDTO = await _bookService.GetById(id);
        return responseBookDTO;
    }

    [HttpPost]
    public async Task<ActionResult> Create(BookPostDTO bookPostDTO) {
        var available = await _bookService.AvailabilityIsbn(bookPostDTO.Isbn);
        var valid = await _bookService.ValidatePrice(bookPostDTO.Preco);

        if (!available){
            return BadRequest("O Isbn não pode ser repetido.");
        }
        if (!valid) {
            return BadRequest("O preço não pode ser negativo.");
        }

        var bookDTOCreated = await _bookService.Create(bookPostDTO);
        return bookDTOCreated == null ? BadRequest("Ocorreu um erro ao adicionar o livro.") : Ok("Livro adicionado com sucesso!");
    }

    [HttpPut]
    public async Task<ActionResult> Update(BookDTO bookDTO) {
        var available = await _bookService.AvailabilityIsbn(bookDTO.Isbn);
        var valid = await _bookService.ValidatePrice(bookDTO.Preco);

        if (!available) {
            return BadRequest("O Isbn não pode ser repetido.");
        }
        if (!valid) {
            return BadRequest("O preço não pode ser negativo.");
        }

        var bookDTOUpdated = await _bookService.Update(bookDTO);
        return bookDTOUpdated == null ? BadRequest("Ocorreu um erro ao alterar o livro.") : Ok("Livro alterado com sucesso!");
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id) {
        var bookDTODeleted = await _bookService.Delete(id);
        return bookDTODeleted == null ? BadRequest("Ocorreu um erro ao eliminar o livro.") : Ok("Livro eliminado com sucesso!");
    }

}*/

    //private readonly BookContext _context;

    /*public LivroController(LivroContext context) {
        _context = context;
    }
    public BookController(IBookCRUD livroCRUD) {
        _bookCRUD = livroCRUD;
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

            var obterLivro = new LivroDTO { Isbn = livro.Isbn, Nome = livro.Nome, Autor = livro.Autor, Preco = livro.Preco };

            return obterLivro == null ? NotFound("Livro não encontrado") : Ok(obterLivro); 
        }
        //Criar livro
        [HttpPost]
        public IActionResult Create() {
            var livroCriado = true;
           // if (livroCriado) {
           //     _createLivro.CreateLivro("");
           // }
           // return Ok();

        } */

}
