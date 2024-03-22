using Azure;
using BookSamsys.BLL.Services;
using BookSamsys.DAL.Context;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Interfaces.Books;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BookSamsys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase {

        private readonly IBookService _bookService;
        private readonly BookContext? _context;

        public BookController(IBookService bookService, BookContext bookContext) {
            _bookService = bookService;
            _context = bookContext;
        }

        [HttpGet("pg")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllPag(int pageNumber, int pageQuantity) {
            var responseBooksDTOGetAllPag = await _bookService.GetAllPag(pageNumber, pageQuantity);
            return responseBooksDTOGetAllPag.Success == false ? BadRequest(responseBooksDTOGetAllPag.Message) : Ok(responseBooksDTOGetAllPag);
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll() {
            var responseBooksDTOGetAll = await _bookService.GetAll();
            return responseBooksDTOGetAll.Success == false ? BadRequest(responseBooksDTOGetAll.Message) : Ok(responseBooksDTOGetAll);
        }
        /*[HttpGet("{page}")]
        public async Task<ActionResult<List<BookDTO>>> GetAll(int page) {
            if (_context.Livros == null) return NotFound();

            var pageResults = 3f; //3 livros
            var pageCount = Math.Ceiling(_context.Livros.Count() / pageResults);
            var totalCount = _context.Livros.Count();

            var books = await _context.Livros
                .Skip((page - 1) * (int)pageResults) //parse
                .Take((int)pageResults)
                .ToListAsync();

            var response = new BookResponse {
                Books = books,
                CurrentPage = page,
                Pages = (int)pageCount,
                Total = (int)totalCount
            };

            return Ok(response);
        }*/

        /*[HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetById(int id) {
            var responseBookDTOGetById = await _bookService.GetById(id);
            return responseBookDTOGetById.Success == false ? BadRequest(responseBookDTOGetById.Message) : Ok(responseBookDTOGetById.Obj);
        }*/

        [HttpPost]
        public async Task<ActionResult<BookPostDTO>> Create(BookPostDTO bookPostDTO) {
            //var responseAvailable = await _bookService.AvailabilityIsbn(bookPostDTO.Isbn);
            //var responseValid = await _bookService.ValidatePrice(bookPostDTO.Preco);
            var responseBookDTOCreated = await _bookService.Create(bookPostDTO);
            return responseBookDTOCreated.Success == false ? BadRequest(responseBookDTOCreated.Message) : Ok(responseBookDTOCreated.Message);
        }
        
        [HttpPut]
        public async Task<ActionResult> Update(BookDTO bookDTO) {
            var responseBookDTOUpdated = await _bookService.Update(bookDTO);
            return responseBookDTOUpdated.Success == false ? BadRequest(responseBookDTOUpdated.Message) : Ok(responseBookDTOUpdated.Message); ;
        }

        [HttpDelete]
        public async Task<ActionResult<BookDTO>> Delete(int id) {
            var responseBookDTODeleted = await _bookService.Delete(id);
            return responseBookDTODeleted.Success == false ? BadRequest(responseBookDTODeleted.Message) : Ok(responseBookDTODeleted.Message);
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
