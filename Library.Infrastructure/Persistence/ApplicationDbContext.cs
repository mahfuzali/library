using Library.Domain.Common;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Infrastructure.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        //public DbSet<Genre> Genres { get; set; }
        //public DbSet<Language> Languages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            /*
            modelBuilder.Entity<Book>()
                .HasMany(c => c.Genres)
                .WithOne();
            */

            modelBuilder.Entity<BookAuthor>()
               .HasKey(x => new { x.BookId, x.AuthorId });

            /**/
            modelBuilder.Entity<BookAuthor>()
                .HasOne<Book>(pt => pt.Book)
                .WithMany(p => p.BookAuthors)
                .HasForeignKey(pt => pt.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne<Author>(pt => pt.Author)
                .WithMany(t => t.BookAuthors)
                .HasForeignKey(pt => pt.AuthorId);

            //Language l = Language.For("English");

            Book b1 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "Mindset: The New Psychology of Success",
                Description = "Dweck explains why it’s not just our abilities and talent that bring us success–but whether we approach them with a fixed or growth mindset. She makes clear why praising intelligence and ability doesn’t foster self-esteem and lead to accomplishment, but may actually jeopardize success. With the right mindset, we can motivate our kids and help them to raise their grades, as well as reach our own goals–personal and professional. Dweck reveals what all great parents, teachers, CEOs, and athletes already know: how a simple idea about the brain can create a love of learning and a resilience that is the basis of great accomplishment in every area.",
                Publisher = "Ballantine Books",
                ISBN = "978-0345472328",
                /*
                Genres = new List<Genre>()
                {
                    new Genre("Psychology")
                },*/
                //Language = l
            };

            Book b2 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "All the President's Men",
                Description = "It began with a break-in at the Democratic National Committee headquarters in Washington DC, on 17 June 1972. Bob Woodward, a journalist for the Washington Post, was called into the office on a Saturday morning to cover the story. Carl Bernstein, a political reporter on the Post, was also assigned. They soon learned this was no ordinary burglary. Following lead after lead, Woodward and Bernstein picked up a trail of money, conspiracy and high-level pressure that ultimately led to the doors of the Oval Office. Men very close to the President were implicated, and then Richard Nixon himself. Over a period of months, Woodward met secretly with Deep Throat, for decades the most famous anonymous source in the history of journalism. As he and Bernstein pieced the jigsaw together, they produced a series of explosive stories that would not only win the Post a Pulitzer Prize, they would bring about the President's scandalous downfall. ALL THE PRESIDENT'S MEN documents this amazing story. Taut, gripping and fascinating, it is a classic of its kind -- the true story of the events that changed the American presidency.",
                Publisher = "Simon & Schuster UK",
                ISBN = "080-7897015427",
                /*
                Genres = new List<Genre>()
                {
                    new Genre("Politics")
                },*/

                //Language = l
            };

            Author a1 = new Author()
            {
                AuthorId = Guid.NewGuid(),
                FirstName = "Carol",
                LastName = "S. Dweck",
                DateOfBirth = new DateTime(1946, 10, 17)
            };
            Author a2 = new Author()
            {
                AuthorId = Guid.NewGuid(),
                FirstName = "Robert",
                LastName = "Upshur Woodward",
                DateOfBirth = new DateTime(1943, 03, 26)
            };
            Author a3 = new Author() 
            {
                AuthorId = Guid.NewGuid(),
                FirstName = "Carl",
                LastName = "Bernstein",
                DateOfBirth = new DateTime(1944, 02, 14)
            };

            
            BookAuthor ba1 = new BookAuthor() 
            {
                BookId = b1.BookId,
                AuthorId = a1.AuthorId
            };

            
            BookAuthor ba2 = new BookAuthor()
            {
                BookId = b2.BookId,
                AuthorId = a2.AuthorId
            };

            BookAuthor ba3 = new BookAuthor() 
            {
                BookId = b2.BookId,
                AuthorId = a3.AuthorId
            };

            modelBuilder.Entity<Book>().HasData(
                b1,
                b2
            );



            modelBuilder.Entity<Author>().HasData(
                a1,
                a2,
                a3
            );
            
            modelBuilder.Entity<BookAuthor>().HasData(
                ba1,
                ba2,
                ba3
            );

            // https://wildermuth.com/2018/08/12/Seeding-Related-Entities-in-EF-Core-2-1-s-HasData()
            /**/
            modelBuilder.Entity<Book>()
                .OwnsOne(l => l.Language)
                .HasData(
                    new { BookId = b1.BookId, Name = "English" },
                    new { BookId = b2.BookId, Name = "English" }
            );
            
            /**/
            modelBuilder.Entity<Book>()
                .OwnsMany(l => l.Genres)
                .HasData(
                    new { Id = 1, BookId = b1.BookId, Name = "Psychology" },
                    new { Id = 2, BookId = b1.BookId, Name = "Neuroscience" },
                    new { Id = 3, BookId = b2.BookId, Name = "Politics" },
                    new { Id = 4, BookId = b2.BookId, Name = "Thriller" }
            );
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
