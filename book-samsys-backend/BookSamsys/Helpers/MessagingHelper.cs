using BookSamsys.BLL.Services;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using BookSamsys.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BookSamsys.Helpers {
    public class MessagingHelper<BookDTO>  {

        private readonly IBookService _bookService;
        public bool Success;
        public string Message;
        public object Obg;

        public MessagingHelper() {
        }

        //private bool Success;
        //private string Message;
        public MessagingHelper(IBookService bookService) {
            _bookService = bookService;
        }



        public async Task<MessagingHelper<BookDTO>> GetAll(int id) {
            MessagingHelper<BookDTO> response = new();
            try {
                var booksDTO = await _bookService.GetAll();
                if (booksDTO == null) {
                    response.Success = false;
                    response.Message = "Não foram encontrados livros.";
                    return response;
                }
                response.Obj = new BookDTO(booksDTO);
                response.Success = true;
            } catch (Exception ex) {
                Logs.Write(LogTypes.Error, $"Falhou ao obter o cliente: {ex.GetBaseException().Message}");

                response.Success = false;
                response.Message = "Ocorreu um erro inesperado ao obter o cliente";
            }
            return response;
        }
    }
}
