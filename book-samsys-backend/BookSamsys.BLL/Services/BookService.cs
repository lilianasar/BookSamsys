using AutoMapper;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using BookSamsys.Infrastructure.Interfaces.Books;
using System.Linq.Expressions;

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

        public async Task<MessagingHelper<IEnumerable<BookDTO>>> GetAllPag(int pageNumber, int pageQuantity) {
            MessagingHelper<IEnumerable<BookDTO>> response = new();
            try { 
                var books = await _repository.GetAllPag(pageNumber, pageQuantity);
                var get = await _repository.GetAll();
                var total = get.Count();
                var totalRows = get.Count();
                if (books == null) {
                    response.Success = false;
                    response.Message = "Não foram encontrados livros.";
                    return response;
                }
                response.Obj = _mapper.Map<IEnumerable<BookDTO>>(books);
                response.Success = true;
                response.Message = "Os livros existentes são os seguintes:";
                response.pageQuantity = pageQuantity;
                response.pageNumber = pageNumber;
                response.totalRows = total;
                return response;
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Ocorreu um erro ao obter os livros. Erro: " + ex.Message;
            }
            return response;
        }
        public async Task<MessagingHelper<IEnumerable<BookDTO>>> GetAll() {
            MessagingHelper<IEnumerable<BookDTO>> response = new();
            try {
                var books = await _repository.GetAll();
                if (books == null) {
                    response.Success = false;
                    response.Message = "Não foram encontrados livros.";
                    return response;
                }
                response.Obj = _mapper.Map<IEnumerable<BookDTO>>(books);
                response.Success = true;
                response.Message = "Os livros existentes são os seguintes:";
                return response;
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Ocorreu um erro ao obter os livros. Erro: " + ex.Message;
            }
            return response;
        }
        public async Task<MessagingHelper<BookDTO>> GetById(int id) {
            MessagingHelper<BookDTO> response = new();
            try { 
                var book = await _repository.GetById(id);
                if (book == null) {
                    response.Success = false;
                    response.Message = "Livro não encontrado.";
                    return response;
                }
                response.Obj = _mapper.Map<BookDTO>(book);
                response.Success = true;
                response.Message = "O livro de Id " + id + " é: ";
                return response;
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Ocorreu um erro ao obter o livro. Erro: " + ex.Message;
            }
            return response;
        }

        //Não aparecer id, pois é automático
        public async Task<MessagingHelper<BookPostDTO>> Create(BookPostDTO bookPostDTO) {
            var book = _mapper.Map<Book>(bookPostDTO);
            MessagingHelper<BookPostDTO> response = new();
            try {
                var availabledIsbn = await _repository.AvailabilityIsbn(book.Isbn, book.Id);
                var validatedPrice = book.Preco >= 0;
                if (availabledIsbn == false || validatedPrice == false) {
                    //bookCreated = null;
                    if (availabledIsbn == false) {
                        response.Success = false;
                        response.Message = "O Isbn não pode ser repetido.";
                        return response;
                    } else if (validatedPrice == false) {
                        response.Success = false;
                        response.Message = "O preço não pode ser negativo.";
                        return response;
                    }
                }
                if (bookPostDTO.Isbn == "" || bookPostDTO.Nome == "" || bookPostDTO.Autor == "") {
                    response.Success = false;
                    response.Message = "Preencha todos os campos.";
                    return response;
                }
                /*if(bookCreated == null) {
                    response.Success = false;
                    response.Message = "Não foi possível criar o livro.";
                    return response;
                } else { 
                */
                //converte livro alterado para livroDTO
                var bookCreated = await _repository.Create(book);
                response.Obj = _mapper.Map<BookPostDTO>(bookCreated);
                response.Success = true;
                response.Message = "Livro criado com sucesso!";
                
            } catch (Exception ex) {
                response.Success = false;
                response.Message = "Ocorreu um erro ao inserir o livro. Erro: " + ex.Message;  
            }
            return response;
        }

        public async Task<MessagingHelper<BookDTO>> Update(BookDTO bookDTO) {
            var book = _mapper.Map<Book>(bookDTO);
            MessagingHelper<BookDTO> response = new();
            try {
                var availabledIsbn = await _repository.AvailabilityIsbn(book.Isbn, book.Id);
                var validatedPrice = book.Preco >= 0;
                if (availabledIsbn == false || validatedPrice == false) {
                    //bookUpdated = null;
                    if (availabledIsbn == false) {
                        response.Success = false;
                        response.Message = "O Isbn não pode ser repetido.";
                        return response;
                    } else if (validatedPrice == false) {
                        response.Success = false;
                        response.Message = "O preço não pode ser negativo.";
                        return response;
                    }
                }
                if (book.Isbn == "" || book.Nome == "" || book.Autor == "" ) {
                    response.Success = false;
                    response.Message = "Preencha todos os campos.";
                    return response;
                }
                var RepeatedData = await _repository.RepeatedData(book.Isbn, book.Nome, book.Autor, book.Preco);
                if(RepeatedData == true) {
                    response.Success = false;
                    response.Message = "Os dados não podem ser repetidos.";
                    return response;
                }

                //update
                var bookUpdated = await _repository.Update(book);
                if (bookUpdated == null) {
                    response.Success = false;
                    response.Message = "O livro não foi atualizado.";
                    return response;
                }
                //converte livro alterado para livroDTO
                response.Obj = _mapper.Map<BookDTO>(bookUpdated);
                response.Success = true;
                response.Message = "Livro atualizado com sucesso!";

            }catch(Exception ex) {
                response.Success = false;
                response.Message = "Ocorreu um erro ao atualizar o livro. Erro: " + ex.Message;
            }
            return response;
        }

        public async Task<MessagingHelper<BookDTO>> Delete(int id) {
            MessagingHelper<BookDTO> response = new();
            try { 
                var bookDeleted = await _repository.Delete(id);
                if (bookDeleted == null) {
                    response.Success = false;
                    response.Message = "O livro não foi eliminado.";
                    return response;
                }
                //converte livro alterado para livroDTO
                response.Obj = _mapper.Map<BookDTO>(bookDeleted);
                response.Success = true;
                response.Message = "Livro eliminado com sucesso!";
                return response;
            }catch (Exception ex) {
                response.Success = false;
                response.Message = "Ocorreu um erro ao eliminar o livro. Erro: " + ex.Message;
            }
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
