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
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.DateTimeToString()))
                .ForMember(
                    dest => dest.DateOfDeath,
                    opt => opt.MapFrom(src => src.DateOfDeath.DateTimeToString()))
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
                .ReverseMap()
                .ForAllMembers(src => src.Ignore());

        }
    }
}
