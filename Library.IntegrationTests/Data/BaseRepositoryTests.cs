using Library.Application.Common.Interfaces;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.IntegrationTests.Data
{
    public abstract class BaseRepositoryTests
    {
        protected ApplicationDbContext _dbContext;

        protected static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("LibraryDbTests")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        //protected BookRepository GetBookRepository()
        //{
        //    var options = CreateNewContextOptions();
        //    var mockCurrentUserService = new Mock<ICurrentUserService>();
        //    var mockDateTime = new Mock<IDateTime>();
        //    //var mockCurrentTransaction = new Mock<IDbContextTransaction>();

        //    _dbContext = new ApplicationDbContext(
        //        options, mockCurrentUserService.Object, mockDateTime.Object);

        //    var mockPropertyMappingService = new Mock<IPropertyMappingService>();

        //    return new BookRepository(_dbContext, mockPropertyMappingService.Object);
        //}

        protected AuthorRepository GetRepository()
        {
            var options = CreateNewContextOptions();
            var mockCurrentUserService = new Mock<ICurrentUserService>();
            var mockDateTime = new Mock<IDateTime>();

            _dbContext = new ApplicationDbContext(
                options, mockCurrentUserService.Object, mockDateTime.Object);

            var mockPropertyMappingService = new Mock<IPropertyMappingService>();

            return new AuthorRepository(_dbContext, mockPropertyMappingService.Object);
        }
    }
}
