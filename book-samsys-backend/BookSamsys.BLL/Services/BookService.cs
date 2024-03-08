using AutoMapper;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using BookSamsys.Infrastructure.Interfaces;

namespace BookSamsys.BLL.Services {
    public class BookService : IBookService {

        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        //Recebe repositorio e atribui a _repository
        public BookService(IBookRepository repository, IMapper mapper) {  
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDTO>> GetAll() {
            var books = await _repository.GetAll();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDTO> GetById(int id) {
            var book = await _repository.GetById(id);
            return _mapper.Map<BookDTO>(book);
        }

        //Não aparecer id, pois é automático
        public async Task<BookPostDTO> Create(BookPostDTO bookPostDTO) {
            var book = _mapper.Map<Book>(bookPostDTO);
            var bookCreated = await _repository.Create(book);
            //converte livro alterado para livroDTO
            return _mapper.Map<BookPostDTO>(bookCreated);
        }

        public async Task<BookDTO> Update(BookDTO bookDTO) {
            var book = _mapper.Map<Book>(bookDTO);
            var bookUpdated = await _repository.Update(book);
            //converte livro alterado para livroDTO
            return _mapper.Map<BookDTO>(bookUpdated);
        }

        public async Task<BookDTO> Delete(int id) {
            var bookDeleted = await _repository.Delete(id);
            //converte livro alterado para livroDTO
            return _mapper.Map<BookDTO>(bookDeleted);
        }
    }
}
