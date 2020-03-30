using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Library.Application.Dtos.Models;
using Library.Domain.Entities;

namespace Library.Application.Dtos.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile() {

            CreateMap<Book, BookDto>()
                .ForMember(
                    dest => dest.Authors,
                    opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Author)))
                .ReverseMap()
                .ReverseMap();

            CreateMap<Book, BookViewModel>()
                .ReverseMap();
        }
        
    }
}
