using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(nullable: false),
                    DateOfDeath = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1500, nullable: false),
                    Publisher = table.Column<string>(nullable: false),
                    ISBN = table.Column<string>(nullable: true),
                    Language_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(maxLength: 200, nullable: true),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Data = table.Column<string>(maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    BookId = table.Column<Guid>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => new { x.BookId, x.Id });
                    table.ForeignKey(
                        name: "FK_Genre_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Created", "CreatedBy", "DateOfBirth", "DateOfDeath", "FirstName", "LastModified", "LastModifiedBy", "LastName" },
                values: new object[,]
                {
                    { new Guid("0775a1ad-4eec-4c4a-bddd-85f49b1bcabe"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1946, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Carol", null, null, "S. Dweck" },
                    { new Guid("02ec09a0-463c-4c89-98d0-77e289bdab3c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1943, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Robert", null, null, "Upshur Woodward" },
                    { new Guid("421349de-7aea-4d7a-b4bb-887da6300070"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1944, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Carl", null, null, "Bernstein" },
                    { new Guid("1a97a240-3b68-4c97-bfa0-08764059aa7c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "J. K.", null, null, "Rowling" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "Publisher", "Title", "Language_Name" },
                values: new object[,]
                {
                    { new Guid("d541e0ff-adc6-423c-af08-5825fb2f93b9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dweck explains why it’s not just our abilities and talent that bring us success–but whether we approach them with a fixed or growth mindset. She makes clear why praising intelligence and ability doesn’t foster self-esteem and lead to accomplishment, but may actually jeopardize success. With the right mindset, we can motivate our kids and help them to raise their grades, as well as reach our own goals–personal and professional. Dweck reveals what all great parents, teachers, CEOs, and athletes already know: how a simple idea about the brain can create a love of learning and a resilience that is the basis of great accomplishment in every area.", "978-0345472328", null, null, "Ballantine Books", "Mindset: The New Psychology of Success", "English" },
                    { new Guid("03e71245-0de5-48a0-b605-0ccddfc54e43"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "It began with a break-in at the Democratic National Committee headquarters in Washington DC, on 17 June 1972. Bob Woodward, a journalist for the Washington Post, was called into the office on a Saturday morning to cover the story. Carl Bernstein, a political reporter on the Post, was also assigned. They soon learned this was no ordinary burglary. Following lead after lead, Woodward and Bernstein picked up a trail of money, conspiracy and high-level pressure that ultimately led to the doors of the Oval Office. Men very close to the President were implicated, and then Richard Nixon himself. Over a period of months, Woodward met secretly with Deep Throat, for decades the most famous anonymous source in the history of journalism. As he and Bernstein pieced the jigsaw together, they produced a series of explosive stories that would not only win the Post a Pulitzer Prize, they would bring about the President's scandalous downfall. ALL THE PRESIDENT'S MEN documents this amazing story. Taut, gripping and fascinating, it is a classic of its kind -- the true story of the events that changed the American presidency.", "080-7897015427", null, null, "Simon & Schuster UK", "All the President's Men", "English" },
                    { new Guid("c57478cf-ae5a-408d-9b21-9d23c39b6d53"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Harry Potter has never even heard of Hogwarts when the letters start dropping on the doormat at number four, Privet Drive. Addressed in green ink on yellowish parchment with a purple seal, they are swiftly confiscated by his grisly aunt and uncle. Then, on Harry’s eleventh birthday, a great beetle-eyed giant of a man called Rubeus Hagrid bursts in with some astonishing news: Harry Potter is a wizard, and he has a place at Hogwarts School of Witchcraft and Wizardry. An incredible adventure is about to begin!", "978-1408855898", null, null, "Bloomsbury Children's Books", "Harry Potter and the Philosopher's Stone", "English" },
                    { new Guid("7f69e86b-14f7-40be-835f-21394e47bbc9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Harry Potter’s summer has included the worst birthday ever, doomy warnings from a house-elf called Dobby, and rescue from the Dursleys by his friend Ron Weasley in a magical flying car! Back at Hogwarts School of Witchcraft and Wizardry for his second year, Harry hears strange whispers echo through empty corridors – and then the attacks start. Students are found as though turned to stone … Dobby’s sinister predictions seem to be coming true.", "978-1408855904", null, null, "Bloomsbury Children's Books", "Harry Potter and the Chamber of Secrets", "English" },
                    { new Guid("8ef2172a-3e52-46d9-bff3-9d2881c5f721"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "When the Knight Bus crashes through the darkness and screeches to a halt in front of him, it’s the start of another far from ordinary year at Hogwarts for Harry Potter. Sirius Black, escaped mass-murderer and follower of Lord Voldemort, is on the run – and they say he is coming after Harry. In his first ever Divination class, Professor Trelawney sees an omen of death in Harry’s tea leaves … But perhaps most terrifying of all are the Dementors patrolling the school grounds, with their soul-sucking kiss.", "978-1408855911", null, null, "Bloomsbury Children's Books", "Harry Potter and the Prisoner of Azkaban", "English" },
                    { new Guid("37ca9d6d-9f71-439a-8bdc-5f814e5975e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "The Triwizard Tournament is to be held at Hogwarts. Only wizards who are over seventeen are allowed to enter – but that doesn’t stop Harry dreaming that he will win the competition. Then at Hallowe’en, when the Goblet of Fire makes its selection, Harry is amazed to find his name is one of those that the magical cup picks out. He will face death-defying tasks, dragons and Dark wizards, but with the help of his best friends, Ron and Hermione, he might just make it through – alive!", "978-1408855928", null, null, "Bloomsbury Children's Books", "Harry Potter and the Goblet of Fire", "English" },
                    { new Guid("c17ab212-156a-40d2-a871-5122223fd1d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dark times have come to Hogwarts. After the Dementors’ attack on his cousin Dudley, Harry Potter knows that Voldemort will stop at nothing to find him. There are many who deny the Dark Lord’s return, but Harry is not alone: a secret order gathers at Grimmauld Place to fight against the Dark forces. Harry must allow Professor Snape to teach him how to protect himself from Voldemort’s savage assaults on his mind. But they are growing stronger by the day and Harry is running out of time.", "978-1408855935", null, null, "Bloomsbury Children's Books", "Harry Potter and the Order of the Phoenix", "English" },
                    { new Guid("31c48452-dc45-4a2f-92ec-3d0b2a8a0ee9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "When Dumbledore arrives at Privet Drive one summer night to collect Harry Potter, his wand hand is blackened and shrivelled, but he does not reveal why. Secrets and suspicion are spreading through the wizarding world, and Hogwarts itself is not safe. Harry is convinced that Malfoy bears the Dark Mark: there is a Death Eater amongst them. Harry will need powerful magic and true friends as he explores Voldemort’s darkest secrets, and Dumbledore prepares him to face his destiny.", "978-1408855942", null, null, "Bloomsbury Children's Books", "Harry Potter and the Half-Blood Prince", "English" },
                    { new Guid("8c26a7b6-8b94-4039-b81e-d664b444711a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "As he climbs into the sidecar of Hagrid’s motorbike and takes to the skies, leaving Privet Drive for the last time, Harry Potter knows that Lord Voldemort and the Death Eaters are not far behind. The protective charm that has kept Harry safe until now is now broken, but he cannot keep hiding. The Dark Lord is breathing fear into everything Harry loves, and to stop him Harry will have to find and destroy the remaining Horcruxes. The final battle must begin – Harry must stand and face his enemy.", "978-1408855959", null, null, "Bloomsbury Children's Books", "Harry Potter and the Deathly Hallows", "English" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId", "Created", "CreatedBy", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { new Guid("d541e0ff-adc6-423c-af08-5825fb2f93b9"), new Guid("0775a1ad-4eec-4c4a-bddd-85f49b1bcabe"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("31c48452-dc45-4a2f-92ec-3d0b2a8a0ee9"), new Guid("1a97a240-3b68-4c97-bfa0-08764059aa7c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("03e71245-0de5-48a0-b605-0ccddfc54e43"), new Guid("02ec09a0-463c-4c89-98d0-77e289bdab3c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("03e71245-0de5-48a0-b605-0ccddfc54e43"), new Guid("421349de-7aea-4d7a-b4bb-887da6300070"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("c17ab212-156a-40d2-a871-5122223fd1d9"), new Guid("1a97a240-3b68-4c97-bfa0-08764059aa7c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("c57478cf-ae5a-408d-9b21-9d23c39b6d53"), new Guid("1a97a240-3b68-4c97-bfa0-08764059aa7c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("7f69e86b-14f7-40be-835f-21394e47bbc9"), new Guid("1a97a240-3b68-4c97-bfa0-08764059aa7c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("8c26a7b6-8b94-4039-b81e-d664b444711a"), new Guid("1a97a240-3b68-4c97-bfa0-08764059aa7c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("8ef2172a-3e52-46d9-bff3-9d2881c5f721"), new Guid("1a97a240-3b68-4c97-bfa0-08764059aa7c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("37ca9d6d-9f71-439a-8bdc-5f814e5975e6"), new Guid("1a97a240-3b68-4c97-bfa0-08764059aa7c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "BookId", "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("31c48452-dc45-4a2f-92ec-3d0b2a8a0ee9"), 10, "Fantasy Fiction" },
                    { new Guid("c17ab212-156a-40d2-a871-5122223fd1d9"), 9, "Fantasy Fiction" },
                    { new Guid("37ca9d6d-9f71-439a-8bdc-5f814e5975e6"), 8, "Fantasy Fiction" },
                    { new Guid("7f69e86b-14f7-40be-835f-21394e47bbc9"), 6, "Fantasy Fiction" },
                    { new Guid("c57478cf-ae5a-408d-9b21-9d23c39b6d53"), 5, "Fantasy Fiction" },
                    { new Guid("03e71245-0de5-48a0-b605-0ccddfc54e43"), 4, "Thriller" },
                    { new Guid("03e71245-0de5-48a0-b605-0ccddfc54e43"), 3, "Politics" },
                    { new Guid("d541e0ff-adc6-423c-af08-5825fb2f93b9"), 2, "Neuroscience" },
                    { new Guid("d541e0ff-adc6-423c-af08-5825fb2f93b9"), 1, "Psychology" },
                    { new Guid("8ef2172a-3e52-46d9-bff3-9d2881c5f721"), 7, "Fantasy Fiction" },
                    { new Guid("8c26a7b6-8b94-4039-b81e-d664b444711a"), 11, "Fantasy Fiction" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
