using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);

            builder.Property(t => t.Title)
                .IsRequired().HasMaxLength(100);

            builder.Property(t => t.Description)
                .IsRequired().HasMaxLength(1500);

            builder.Property(t => t.Publisher)
                .IsRequired().HasMaxLength(100);

            builder.Property(t => t.ISBN)
                .IsRequired().HasMaxLength(14);
        }
    }
}
