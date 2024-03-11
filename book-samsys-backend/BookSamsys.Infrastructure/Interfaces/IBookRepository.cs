using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.Interfaces {
    public interface IBookRepository {
        Task<Book> Create(Book book);
        Task<Book> Update(Book book);
        Task<Book> Delete(int id);
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<bool> AvailabilityIsbn(string isbn);
        Task<bool> ValidatePrice(decimal preco);
    }
}
