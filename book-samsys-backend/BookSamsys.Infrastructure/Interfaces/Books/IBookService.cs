using BookSamsys.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.Interfaces.Books
{
    public interface IBookService
    {
        Task<MessagingHelper<IEnumerable<BookDTO>>> GetAll();
        Task<MessagingHelper<BookDTO>> GetById(int id);
        Task<MessagingHelper<BookPostDTO>> Create(BookPostDTO bookPostDTO);
        Task<MessagingHelper<BookDTO>> Update(BookDTO bookDTO);
        Task<MessagingHelper<BookDTO>> Delete(int id);
        //Task<MessagingHelper<bool>> AvailabilityIsbn(string isbn);
        Task<MessagingHelper<bool>> ValidatePrice(decimal preco);
    }
}
