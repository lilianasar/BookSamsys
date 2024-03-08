using AutoMapper;
using BookSamsys.Infrastructure.DTOs;
using BookSamsys.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSamsys.Infrastructure.Mappings {
    public class EntityToDTOMappingProfile : Profile {

        public EntityToDTOMappingProfile() { 
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, BookPostDTO>().ReverseMap();
        }
    }
}
