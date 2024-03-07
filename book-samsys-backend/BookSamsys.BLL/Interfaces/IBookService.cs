using AutoMapper;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.BLL.Interfaces
{
    public interface IBookService {
        Task<BookDTO> Create(BookDTO bookDTO);
        Task<BookDTO> Update(BookDTO bookDTO);
        Task<BookDTO> Delete(int id);
        Task<List<BookDTO>> GetAll();
        Task<BookDTO> GetById(int id);
    }
}
