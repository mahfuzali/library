using Library.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;

namespace Library.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        private static readonly object _appDbContextSeedLock = new object();

        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "mahfuzali", SecurityStamp = Guid.NewGuid().ToString(), Email = "mahfuz.ali@hitachivantara.com" };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "Library!1");
            }
        }
        
        public static void SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            lock (_appDbContextSeedLock)
            {
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>() {
                        SeedData.book1,
                        SeedData.book2,
                        SeedData.harryPotterBook1,
                        SeedData.harryPotterBook2,
                        SeedData.harryPotterBook3,
                        SeedData.harryPotterBook4,
                        SeedData.harryPotterBook5,
                        SeedData.harryPotterBook6, 
                        SeedData.harryPotterBook7
                    });
                }

                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(new List<Author>() {
                        SeedData.author1,
                        SeedData.author2,
                        SeedData.author3,
                        SeedData.harryPotterAuthor
                    });
                }

                if (!context.BookAuthors.Any())
                {
                    context.BookAuthors.AddRange(new List<BookAuthor>() {
                        SeedData.bookAuthor1,
                        SeedData.bookAuthor2,
                        SeedData.bookAuthor3,
                        SeedData.bookAuthorHarry1,
                        SeedData.bookAuthorHarry2,
                        SeedData.bookAuthorHarry3,
                        SeedData.bookAuthorHarry4,
                        SeedData.bookAuthorHarry5,
                        SeedData.bookAuthorHarry6,
                        SeedData.bookAuthorHarry7
                    });
                }

                context.SaveChangesAsync();
            }
        }
    }
}
