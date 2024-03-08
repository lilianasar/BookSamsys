using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.Interfaces {
    public interface IBookService {
        Task<BookPostDTO> Create(BookPostDTO bookPostDTO);
        Task<BookDTO> Update(BookDTO bookDTO);
        Task<BookDTO> Delete(int id);
        Task<IEnumerable<BookDTO>> GetAll();
        Task<BookDTO> GetById(int id);
    }
}
