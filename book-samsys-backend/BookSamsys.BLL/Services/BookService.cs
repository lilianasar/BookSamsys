using AutoMapper;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using BookSamsys.Infrastructure.Interfaces;

namespace BookSamsys.BLL.Services
{
    public class BookService : IBookService {

        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        //Recebe repositorio e atribui a _repository
        public BookService(IBookRepository repository, IMapper mapper) {  
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MessagingHelper<IEnumerable<BookDTO>>> GetAll() {
            MessagingHelper<IEnumerable<BookDTO>> response = new();
            var books = await _repository.GetAll();
            if (books == null) {
                response.Success = false;
                response.Message = "Não foram encontrados livros.";
                return response;
            }
            response.Success = true;
            response.Obj = _mapper.Map<IEnumerable<BookDTO>>(books);
            return response;
        }

        public async Task<MessagingHelper<BookDTO>> GetById(int id) {
            MessagingHelper<BookDTO> response = new();
            var book = await _repository.GetById(id);
            if(book == null) {
                response.Success = false;
                response.Message = "Livro não encontrado.";
                return response;
            }
            response.Success = true;
            response.Obj = _mapper.Map<BookDTO>(book);
            return response;
        }

        //Não aparecer id, pois é automático
        public async Task<MessagingHelper<BookPostDTO>> Create(BookPostDTO bookPostDTO) {
            var book = _mapper.Map<Book>(bookPostDTO);
            MessagingHelper<BookPostDTO> response = new();
            var availabledIsbn = await _repository.AvailabilityIsbn(book.Isbn);
            var validatedPrice = book.Preco >= 0;
            var bookCreated = await _repository.Create(book);
            if(availabledIsbn == false || validatedPrice == false) {
                bookCreated = null;
                if (availabledIsbn == false) {
                    response.Success = false;
                    response.Message = "O Isbn não pode ser repetido.";
                    return response;
                } else if(validatedPrice == false) {
                    response.Success = false;
                    response.Message = "O preço não pode ser negativo.";
                    return response;
                }
            }
            if (bookCreated == null) {
                response.Success = false;
                response.Message = "O livro não foi criado.";
                return response;
            } 
            response.Success = true;
            //converte livro alterado para livroDTO
            response.Obj = _mapper.Map<BookPostDTO>(bookCreated);   
            return response;
            
        }

        public async Task<MessagingHelper<BookDTO>> Update(BookDTO bookDTO) {
            var book = _mapper.Map<Book>(bookDTO);
            MessagingHelper<BookDTO> response = new();
            var bookUpdated = await _repository.Update(book);
            if(bookUpdated == null) {
                response.Success = false;
                response.Message = "O livro não foi atualizado.";
                return response;
            }
            response.Success = true;
            //converte livro alterado para livroDTO
            response.Obj = _mapper.Map<BookDTO>(bookUpdated);
            return response;
        }

        public async Task<MessagingHelper<BookDTO>> Delete(int id) {
            MessagingHelper<BookDTO> response = new();
            var bookDeleted = await _repository.Delete(id);
            if( bookDeleted == null) {
                response.Success = false;
                response.Message = "O livro não foi eliminado.";
                return response;
            }
            response.Success= true;
            //converte livro alterado para livroDTO
            response.Obj = _mapper.Map<BookDTO>(bookDeleted);
            return response;
        }

        /*public async Task<MessagingHelper<bool>> AvailabilityIsbn(string isbn) {
            MessagingHelper<bool> response = new();
            var isbnAvailable = await _repository.AvailabilityIsbn(isbn);
            if(isbnAvailable == false) {
                response.Success = false;
                response.Message = "O Isbn não pode ser repetido.";
                return response;
            }
            response.Success= true;
            return response;
        }*/

        public Task<MessagingHelper<bool>> ValidatePrice(decimal preco) {
            var priceValidate = preco < 0;
            MessagingHelper<bool> response = new();
            if (priceValidate == false) {
                response.Success = false;
                response.Message = "O preço não pode ser negativo.";
                return Task.FromResult(response);
            }
            response.Success = true;
            return Task.FromResult(response);

            /*public Task<bool> ValidatePrice(decimal preco) {
                //verifica se o preço é negativo
                var priceValidate = preco < 0;

                return Task.FromResult(!priceValidate);
                //return preco < 0 ? !priceValidate : priceValidate;
            }*/
        }
    }
}
