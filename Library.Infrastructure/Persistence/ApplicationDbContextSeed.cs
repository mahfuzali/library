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
        private static object _appDbContextSeedLock = new object();

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
            Book b1 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "Mindset: The New Psychology of Success",
                Description = "Dweck explains why it’s not just our abilities and talent that bring us success–but whether we approach them with a fixed or growth mindset. She makes clear why praising intelligence and ability doesn’t foster self-esteem and lead to accomplishment, but may actually jeopardize success. With the right mindset, we can motivate our kids and help them to raise their grades, as well as reach our own goals–personal and professional. Dweck reveals what all great parents, teachers, CEOs, and athletes already know: how a simple idea about the brain can create a love of learning and a resilience that is the basis of great accomplishment in every area.",
                Publisher = "Ballantine Books",
                ISBN = "978-0345472328",
                Language = Language.For("English"),
                Genres = new List<Genre>() { Genre.For("Psychology"), Genre.For("Neuroscience") }
            };

            Book b2 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "All the President's Men",
                Description = "It began with a break-in at the Democratic National Committee headquarters in Washington DC, on 17 June 1972. Bob Woodward, a journalist for the Washington Post, was called into the office on a Saturday morning to cover the story. Carl Bernstein, a political reporter on the Post, was also assigned. They soon learned this was no ordinary burglary. Following lead after lead, Woodward and Bernstein picked up a trail of money, conspiracy and high-level pressure that ultimately led to the doors of the Oval Office. Men very close to the President were implicated, and then Richard Nixon himself. Over a period of months, Woodward met secretly with Deep Throat, for decades the most famous anonymous source in the history of journalism. As he and Bernstein pieced the jigsaw together, they produced a series of explosive stories that would not only win the Post a Pulitzer Prize, they would bring about the President's scandalous downfall. ALL THE PRESIDENT'S MEN documents this amazing story. Taut, gripping and fascinating, it is a classic of its kind -- the true story of the events that changed the American presidency.",
                Publisher = "Simon & Schuster UK",
                ISBN = "080-7897015427",
                Language = Language.For("English"),
                Genres = new List<Genre>() { Genre.For("Politics"), Genre.For("Thriller") }
            };

            Book h1 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "Harry Potter and the Philosopher's Stone",
                Description = "Harry Potter has never even heard of Hogwarts when the letters start dropping on the doormat at number four, Privet Drive. Addressed in green ink on yellowish parchment with a purple seal, they are swiftly confiscated by his grisly aunt and uncle. Then, on Harry’s eleventh birthday, a great beetle-eyed giant of a man called Rubeus Hagrid bursts in with some astonishing news: Harry Potter is a wizard, and he has a place at Hogwarts School of Witchcraft and Wizardry. An incredible adventure is about to begin!",
                Publisher = "Bloomsbury Children's Books",
                ISBN = "978-1408855898",
                Language = Language.For("English"),
                Genres = new List<Genre>() { Genre.For("Fantasy Fiction") }
            };
            Book h2 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "Harry Potter and the Chamber of Secrets",
                Description = "Harry Potter’s summer has included the worst birthday ever, doomy warnings from a house-elf called Dobby, and rescue from the Dursleys by his friend Ron Weasley in a magical flying car! Back at Hogwarts School of Witchcraft and Wizardry for his second year, Harry hears strange whispers echo through empty corridors – and then the attacks start. Students are found as though turned to stone … Dobby’s sinister predictions seem to be coming true.",
                Publisher = "Bloomsbury Children's Books",
                ISBN = "978-1408855904",
                Language = Language.For("English"),
                Genres = new List<Genre>() { Genre.For("Fantasy Fiction") }
            };
            Book h3 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "Harry Potter and the Prisoner of Azkaban",
                Description = "When the Knight Bus crashes through the darkness and screeches to a halt in front of him, it’s the start of another far from ordinary year at Hogwarts for Harry Potter. Sirius Black, escaped mass-murderer and follower of Lord Voldemort, is on the run – and they say he is coming after Harry. In his first ever Divination class, Professor Trelawney sees an omen of death in Harry’s tea leaves … But perhaps most terrifying of all are the Dementors patrolling the school grounds, with their soul-sucking kiss.",
                Publisher = "Bloomsbury Children's Books",
                ISBN = "978-1408855911",
                Language = Language.For("English"),
                Genres = new List<Genre>() { Genre.For("Fantasy Fiction") }
            };
            Book h4 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "Harry Potter and the Goblet of Fire",
                Description = "The Triwizard Tournament is to be held at Hogwarts. Only wizards who are over seventeen are allowed to enter – but that doesn’t stop Harry dreaming that he will win the competition. Then at Hallowe’en, when the Goblet of Fire makes its selection, Harry is amazed to find his name is one of those that the magical cup picks out. He will face death-defying tasks, dragons and Dark wizards, but with the help of his best friends, Ron and Hermione, he might just make it through – alive!",
                Publisher = "Bloomsbury Children's Books",
                ISBN = "978-1408855928",
                Language = Language.For("English"),
                Genres = new List<Genre>() { Genre.For("Fantasy Fiction") }
            };
            Book h5 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "Harry Potter and the Order of the Phoenix",
                Description = "Dark times have come to Hogwarts. After the Dementors’ attack on his cousin Dudley, Harry Potter knows that Voldemort will stop at nothing to find him. There are many who deny the Dark Lord’s return, but Harry is not alone: a secret order gathers at Grimmauld Place to fight against the Dark forces. Harry must allow Professor Snape to teach him how to protect himself from Voldemort’s savage assaults on his mind. But they are growing stronger by the day and Harry is running out of time.",
                Publisher = "Bloomsbury Children's Books",
                ISBN = "978-1408855935",
                Language = Language.For("English"),
                Genres = new List<Genre>() { Genre.For("Fantasy Fiction") }
            };
            Book h6 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "Harry Potter and the Half-Blood Prince",
                Description = "When Dumbledore arrives at Privet Drive one summer night to collect Harry Potter, his wand hand is blackened and shrivelled, but he does not reveal why. Secrets and suspicion are spreading through the wizarding world, and Hogwarts itself is not safe. Harry is convinced that Malfoy bears the Dark Mark: there is a Death Eater amongst them. Harry will need powerful magic and true friends as he explores Voldemort’s darkest secrets, and Dumbledore prepares him to face his destiny.",
                Publisher = "Bloomsbury Children's Books",
                ISBN = "978-1408855942",
                Language = Language.For("English"),
                Genres = new List<Genre>() { Genre.For("Fantasy Fiction") }
            };
            Book h7 = new Book()
            {
                BookId = Guid.NewGuid(),
                Title = "Harry Potter and the Deathly Hallows",
                Description = "As he climbs into the sidecar of Hagrid’s motorbike and takes to the skies, leaving Privet Drive for the last time, Harry Potter knows that Lord Voldemort and the Death Eaters are not far behind. The protective charm that has kept Harry safe until now is now broken, but he cannot keep hiding. The Dark Lord is breathing fear into everything Harry loves, and to stop him Harry will have to find and destroy the remaining Horcruxes. The final battle must begin – Harry must stand and face his enemy.",
                Publisher = "Bloomsbury Children's Books",
                ISBN = "978-1408855959",
                Language = Language.For("English"),
                Genres = new List<Genre>() { Genre.For("Fantasy Fiction") }
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
            Author ha = new Author()
            {
                AuthorId = Guid.NewGuid(),
                FirstName = "J. K.",
                LastName = "Rowling",
                DateOfBirth = new DateTime(1965, 07, 31)
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

            BookAuthor bah1 = new BookAuthor()
            {
                BookId = h1.BookId,
                AuthorId = ha.AuthorId
            };
            BookAuthor bah2 = new BookAuthor()
            {
                BookId = h2.BookId,
                AuthorId = ha.AuthorId
            };
            BookAuthor bah3 = new BookAuthor()
            {
                BookId = h3.BookId,
                AuthorId = ha.AuthorId
            };
            BookAuthor bah4 = new BookAuthor()
            {
                BookId = h4.BookId,
                AuthorId = ha.AuthorId
            };
            BookAuthor bah5 = new BookAuthor()
            {
                BookId = h5.BookId,
                AuthorId = ha.AuthorId
            };
            BookAuthor bah6 = new BookAuthor()
            {
                BookId = h6.BookId,
                AuthorId = ha.AuthorId
            };
            BookAuthor bah7 = new BookAuthor()
            {
                BookId = h7.BookId,
                AuthorId = ha.AuthorId
            };

            // Seed, if necessary
            lock (_appDbContextSeedLock)
            {
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>() {
                        b1, b2, h1, h2, h3, h4, h5, h6, h7
                    });
                }
                else
                {
                    foreach (var item in context.Books)
                    {
                        context.Remove(item);
                    }
                }

                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(new List<Author>() {
                        a1, a2, a3, ha
                    });
                }
                else
                {
                    foreach (var item in context.Authors)
                    {
                        context.Remove(item);
                    }
                }

                if (!context.BookAuthors.Any())
                {
                    context.BookAuthors.AddRange(new List<BookAuthor>() {
                        ba1, ba2, ba3, bah1, bah2, bah3, bah4, bah5, bah6, bah7
                    });
                }
                else
                {
                    foreach (var item in context.BookAuthors)
                    {
                        context.Remove(item);
                    }
                }

                context.SaveChangesAsync();
            }
        }
        /**/
    }
}
