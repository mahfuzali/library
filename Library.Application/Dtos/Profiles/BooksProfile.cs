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
                .ForMember(
                    dest => dest.Genres,
                    opt => opt.MapFrom(src => src.Genres.Select(ba => ba.Name)))
                
                .ForMember(
                    dest => dest.Language,
                    opt => opt.MapFrom(src => src.Language.Name))
                .ReverseMap();

            CreateMap<Book, BookViewModel>()
                .ForMember(
                    dest => dest.Language,
                    opt => opt.MapFrom(src => src.Language.Name))
                .ReverseMap();


            CreateMap<Book, BookForCreationDto>()
                .ForMember(
                    dest => dest.Authors,
                    opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Author)))
                .ForMember(
                    dest => dest.Genres,
                    opt => opt.MapFrom(src => src.Genres.Select(ba => ba.Name)))
                .ForMember(
                    dest => dest.Language,
                    opt => opt.MapFrom(src => src.Language.Name))
                .ReverseMap();


            CreateMap<Book, BookForManipulationDto>()
                .ForMember(
                    dest => dest.Authors,
                    opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Author)))
                .ForMember(
                    dest => dest.Genres,
                    opt => opt.MapFrom(src => src.Genres.Select(ba => ba.Name)))
                .ForMember(
                    dest => dest.Language,
                    opt => opt.MapFrom(src => src.Language.Name))
                .ReverseMap();


            CreateMap<Book, BookForCreationForAuthorDto>()
                .ForMember(
                    dest => dest.Genres,
                    opt => opt.MapFrom(src => src.Genres.Select(ba => ba.Name)))
                .ForMember(
                    dest => dest.Language,
                    opt => opt.MapFrom(src => src.Language.Name))
                .ReverseMap();

            CreateMap<Book, BookForUpdateDto>()
                .ForMember(
                    dest => dest.Genres,
                    opt => opt.MapFrom(src => src.Genres.Select(ba => ba.Name)))
                .ForMember(
                    dest => dest.Language,
                    opt => opt.MapFrom(src => src.Language.Name))
                .ReverseMap();

        }
        
    }
}
