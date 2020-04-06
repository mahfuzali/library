using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("2ff0c7ec-598a-4c4c-bf6a-278ce6fca4f1"), new Guid("33f7f5c4-2232-48ca-a40c-76c7d0c66807") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("2ff0c7ec-598a-4c4c-bf6a-278ce6fca4f1"), new Guid("8fa56ea9-3510-4e84-a042-1af4d725f24f") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("b8d9643a-0f9b-4611-85b6-d750bc7468d7"), new Guid("d5775bd6-5465-4015-bab1-0e5416711451") });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("2ff0c7ec-598a-4c4c-bf6a-278ce6fca4f1"), 3 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("2ff0c7ec-598a-4c4c-bf6a-278ce6fca4f1"), 4 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("b8d9643a-0f9b-4611-85b6-d750bc7468d7"), 1 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("b8d9643a-0f9b-4611-85b6-d750bc7468d7"), 2 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("33f7f5c4-2232-48ca-a40c-76c7d0c66807"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("8fa56ea9-3510-4e84-a042-1af4d725f24f"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("d5775bd6-5465-4015-bab1-0e5416711451"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("2ff0c7ec-598a-4c4c-bf6a-278ce6fca4f1"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("b8d9643a-0f9b-4611-85b6-d750bc7468d7"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Authors",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Authors",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Created", "CreatedBy", "DateOfBirth", "DateOfDeath", "FirstName", "LastModified", "LastModifiedBy", "LastName" },
                values: new object[,]
                {
                    { new Guid("3d49160e-4c34-4d26-8b83-109b7742551c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1946, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Carol", null, null, "S. Dweck" },
                    { new Guid("91677be3-f103-493b-9d9d-bdbc5bc505e0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1943, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Robert", null, null, "Upshur Woodward" },
                    { new Guid("4aedb10e-1785-4fea-9565-4614007ece2a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1944, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Carl", null, null, "Bernstein" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "Publisher", "Title", "Language_Name" },
                values: new object[,]
                {
                    { new Guid("d05dcaee-3482-426a-8b42-f8710b8ff876"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dweck explains why it’s not just our abilities and talent that bring us success–but whether we approach them with a fixed or growth mindset. She makes clear why praising intelligence and ability doesn’t foster self-esteem and lead to accomplishment, but may actually jeopardize success. With the right mindset, we can motivate our kids and help them to raise their grades, as well as reach our own goals–personal and professional. Dweck reveals what all great parents, teachers, CEOs, and athletes already know: how a simple idea about the brain can create a love of learning and a resilience that is the basis of great accomplishment in every area.", "978-0345472328", null, null, "Ballantine Books", "Mindset: The New Psychology of Success", "English" },
                    { new Guid("fd053127-8a8a-4d58-ae7c-831aa128025f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "It began with a break-in at the Democratic National Committee headquarters in Washington DC, on 17 June 1972. Bob Woodward, a journalist for the Washington Post, was called into the office on a Saturday morning to cover the story. Carl Bernstein, a political reporter on the Post, was also assigned. They soon learned this was no ordinary burglary. Following lead after lead, Woodward and Bernstein picked up a trail of money, conspiracy and high-level pressure that ultimately led to the doors of the Oval Office. Men very close to the President were implicated, and then Richard Nixon himself. Over a period of months, Woodward met secretly with Deep Throat, for decades the most famous anonymous source in the history of journalism. As he and Bernstein pieced the jigsaw together, they produced a series of explosive stories that would not only win the Post a Pulitzer Prize, they would bring about the President's scandalous downfall. ALL THE PRESIDENT'S MEN documents this amazing story. Taut, gripping and fascinating, it is a classic of its kind -- the true story of the events that changed the American presidency.", "080-7897015427", null, null, "Simon & Schuster UK", "All the President's Men", "English" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId", "Created", "CreatedBy", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { new Guid("d05dcaee-3482-426a-8b42-f8710b8ff876"), new Guid("3d49160e-4c34-4d26-8b83-109b7742551c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("fd053127-8a8a-4d58-ae7c-831aa128025f"), new Guid("91677be3-f103-493b-9d9d-bdbc5bc505e0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("fd053127-8a8a-4d58-ae7c-831aa128025f"), new Guid("4aedb10e-1785-4fea-9565-4614007ece2a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "BookId", "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("d05dcaee-3482-426a-8b42-f8710b8ff876"), 1, "Psychology" },
                    { new Guid("d05dcaee-3482-426a-8b42-f8710b8ff876"), 2, "Neuroscience" },
                    { new Guid("fd053127-8a8a-4d58-ae7c-831aa128025f"), 3, "Politics" },
                    { new Guid("fd053127-8a8a-4d58-ae7c-831aa128025f"), 4, "Thriller" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("d05dcaee-3482-426a-8b42-f8710b8ff876"), new Guid("3d49160e-4c34-4d26-8b83-109b7742551c") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("fd053127-8a8a-4d58-ae7c-831aa128025f"), new Guid("4aedb10e-1785-4fea-9565-4614007ece2a") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("fd053127-8a8a-4d58-ae7c-831aa128025f"), new Guid("91677be3-f103-493b-9d9d-bdbc5bc505e0") });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("d05dcaee-3482-426a-8b42-f8710b8ff876"), 1 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("d05dcaee-3482-426a-8b42-f8710b8ff876"), 2 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("fd053127-8a8a-4d58-ae7c-831aa128025f"), 3 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("fd053127-8a8a-4d58-ae7c-831aa128025f"), 4 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("3d49160e-4c34-4d26-8b83-109b7742551c"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("4aedb10e-1785-4fea-9565-4614007ece2a"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("91677be3-f103-493b-9d9d-bdbc5bc505e0"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("d05dcaee-3482-426a-8b42-f8710b8ff876"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("fd053127-8a8a-4d58-ae7c-831aa128025f"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1500);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Created", "CreatedBy", "DateOfBirth", "DateOfDeath", "FirstName", "LastModified", "LastModifiedBy", "LastName" },
                values: new object[,]
                {
                    { new Guid("d5775bd6-5465-4015-bab1-0e5416711451"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1946, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Carol", null, null, "S. Dweck" },
                    { new Guid("33f7f5c4-2232-48ca-a40c-76c7d0c66807"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1943, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Robert", null, null, "Upshur Woodward" },
                    { new Guid("8fa56ea9-3510-4e84-a042-1af4d725f24f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1944, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Carl", null, null, "Bernstein" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "Publisher", "Title", "Language_Name" },
                values: new object[,]
                {
                    { new Guid("b8d9643a-0f9b-4611-85b6-d750bc7468d7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dweck explains why it’s not just our abilities and talent that bring us success–but whether we approach them with a fixed or growth mindset. She makes clear why praising intelligence and ability doesn’t foster self-esteem and lead to accomplishment, but may actually jeopardize success. With the right mindset, we can motivate our kids and help them to raise their grades, as well as reach our own goals–personal and professional. Dweck reveals what all great parents, teachers, CEOs, and athletes already know: how a simple idea about the brain can create a love of learning and a resilience that is the basis of great accomplishment in every area.", "978-0345472328", null, null, "Ballantine Books", "Mindset: The New Psychology of Success", "English" },
                    { new Guid("2ff0c7ec-598a-4c4c-bf6a-278ce6fca4f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "It began with a break-in at the Democratic National Committee headquarters in Washington DC, on 17 June 1972. Bob Woodward, a journalist for the Washington Post, was called into the office on a Saturday morning to cover the story. Carl Bernstein, a political reporter on the Post, was also assigned. They soon learned this was no ordinary burglary. Following lead after lead, Woodward and Bernstein picked up a trail of money, conspiracy and high-level pressure that ultimately led to the doors of the Oval Office. Men very close to the President were implicated, and then Richard Nixon himself. Over a period of months, Woodward met secretly with Deep Throat, for decades the most famous anonymous source in the history of journalism. As he and Bernstein pieced the jigsaw together, they produced a series of explosive stories that would not only win the Post a Pulitzer Prize, they would bring about the President's scandalous downfall. ALL THE PRESIDENT'S MEN documents this amazing story. Taut, gripping and fascinating, it is a classic of its kind -- the true story of the events that changed the American presidency.", "080-7897015427", null, null, "Simon & Schuster UK", "All the President's Men", "English" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId", "Created", "CreatedBy", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { new Guid("b8d9643a-0f9b-4611-85b6-d750bc7468d7"), new Guid("d5775bd6-5465-4015-bab1-0e5416711451"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("2ff0c7ec-598a-4c4c-bf6a-278ce6fca4f1"), new Guid("33f7f5c4-2232-48ca-a40c-76c7d0c66807"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("2ff0c7ec-598a-4c4c-bf6a-278ce6fca4f1"), new Guid("8fa56ea9-3510-4e84-a042-1af4d725f24f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "BookId", "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("b8d9643a-0f9b-4611-85b6-d750bc7468d7"), 1, "Psychology" },
                    { new Guid("b8d9643a-0f9b-4611-85b6-d750bc7468d7"), 2, "Neuroscience" },
                    { new Guid("2ff0c7ec-598a-4c4c-bf6a-278ce6fca4f1"), 3, "Politics" },
                    { new Guid("2ff0c7ec-598a-4c4c-bf6a-278ce6fca4f1"), 4, "Thriller" }
                });
        }
    }
}
