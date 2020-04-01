using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
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
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(nullable: true),
                    Language_Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
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

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Created", "CreatedBy", "DateOfBirth", "DateOfDeath", "FirstName", "LastModified", "LastModifiedBy", "LastName" },
                values: new object[,]
                {
                    { new Guid("631bfe98-2c6f-4509-be84-73f0f00c5431"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1946, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Carol", null, null, "S. Dweck" },
                    { new Guid("18e75019-5ccc-45e6-88de-78ef0d51633a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1943, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Robert", null, null, "Upshur Woodward" },
                    { new Guid("07dddc67-4984-4ce6-b3a5-aadf317d450b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1944, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Carl", null, null, "Bernstein" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "Publisher", "Title", "Language_Name" },
                values: new object[,]
                {
                    { new Guid("17c13831-6d85-47ba-918e-d2fd9edd631d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dweck explains why it’s not just our abilities and talent that bring us success–but whether we approach them with a fixed or growth mindset. She makes clear why praising intelligence and ability doesn’t foster self-esteem and lead to accomplishment, but may actually jeopardize success. With the right mindset, we can motivate our kids and help them to raise their grades, as well as reach our own goals–personal and professional. Dweck reveals what all great parents, teachers, CEOs, and athletes already know: how a simple idea about the brain can create a love of learning and a resilience that is the basis of great accomplishment in every area.", "978-0345472328", null, null, "Ballantine Books", "Mindset: The New Psychology of Success", "English" },
                    { new Guid("9d2b3951-a00b-4617-8413-feb36914722d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "It began with a break-in at the Democratic National Committee headquarters in Washington DC, on 17 June 1972. Bob Woodward, a journalist for the Washington Post, was called into the office on a Saturday morning to cover the story. Carl Bernstein, a political reporter on the Post, was also assigned. They soon learned this was no ordinary burglary. Following lead after lead, Woodward and Bernstein picked up a trail of money, conspiracy and high-level pressure that ultimately led to the doors of the Oval Office. Men very close to the President were implicated, and then Richard Nixon himself. Over a period of months, Woodward met secretly with Deep Throat, for decades the most famous anonymous source in the history of journalism. As he and Bernstein pieced the jigsaw together, they produced a series of explosive stories that would not only win the Post a Pulitzer Prize, they would bring about the President's scandalous downfall. ALL THE PRESIDENT'S MEN documents this amazing story. Taut, gripping and fascinating, it is a classic of its kind -- the true story of the events that changed the American presidency.", "080-7897015427", null, null, "Simon & Schuster UK", "All the President's Men", "English" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId", "Created", "CreatedBy", "LastModified", "LastModifiedBy" },
                values: new object[] { new Guid("17c13831-6d85-47ba-918e-d2fd9edd631d"), new Guid("631bfe98-2c6f-4509-be84-73f0f00c5431"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId", "Created", "CreatedBy", "LastModified", "LastModifiedBy" },
                values: new object[] { new Guid("9d2b3951-a00b-4617-8413-feb36914722d"), new Guid("18e75019-5ccc-45e6-88de-78ef0d51633a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId", "Created", "CreatedBy", "LastModified", "LastModifiedBy" },
                values: new object[] { new Guid("9d2b3951-a00b-4617-8413-feb36914722d"), new Guid("07dddc67-4984-4ce6-b3a5-aadf317d450b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
