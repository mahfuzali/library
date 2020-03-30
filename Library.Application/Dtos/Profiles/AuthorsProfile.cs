using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Library.Application.Authors.Models;
using Library.Domain.Entities;
using Library.Application.Common.Helpers;
using System.Linq;
using Library.Application.Dtos.Models;

namespace Library.Application.Dtos.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            /**/
            CreateMap<Author, AuthorDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()))
                .ForMember(
                    dest => dest.Books,
                    opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Book)))
                .ReverseMap();

            CreateMap<Author, AuthorViewModel>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()))
                .ReverseMap();

            /*
            CreateMap<Author, AuthorDto>()
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(
                dest => dest.Age,
                opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()))
            .ForMember(
                a => a.Books, 
                opt => opt.MapFrom(
                    a => a.BookAuthors
                            .Select(ba => ba.Book)))
            .ReverseMap()
            .PreserveReferences()
            .ForMember(
                a => a.BookAuthors, 
                opt => opt.MapFrom(
                    a => a.Books
                    .Select(b => new {b.BookId, Book = b, a.AuthorId, Author = a })))
            ;
            */



            /*
            CreateMap<BookAuthor, AuthorDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.BookId));

           
            CreateMap<BookAuthor, BookForAuthorDto>()
               .ForMember(res => res.Id, opt => opt.MapFrom(dto => dto.Book.Id))
               .ForMember(res => res.Name, opt => opt.MapFrom(dto => $"{dto.Author.FirstName} {dto.Author.LastName}"));
            */
        }
    }
}
