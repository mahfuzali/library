using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Created", "CreatedBy", "DateOfBirth", "DateOfDeath", "FirstName", "LastModified", "LastModifiedBy", "LastName" },
                values: new object[,]
                {
                    { new Guid("1d996191-7d04-498d-91bf-2fb4cfbb1d1a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1946, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Carol", null, null, "S. Dweck" },
                    { new Guid("ef0a6dd8-a451-4891-ae04-7d671ce0cac9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1943, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Robert", null, null, "Upshur Woodward" },
                    { new Guid("92d3c08e-335d-4a2e-8e73-ad4f8309146b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1944, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Carl", null, null, "Bernstein" },
                    { new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTimeOffset(new DateTime(1965, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "J. K.", null, null, "Rowling" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Created", "CreatedBy", "Description", "ISBN", "LastModified", "LastModifiedBy", "Publisher", "Title", "Language_Name" },
                values: new object[,]
                {
                    { new Guid("d12bdef9-84f3-4de9-bdec-940f13971782"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dweck explains why it’s not just our abilities and talent that bring us success–but whether we approach them with a fixed or growth mindset. She makes clear why praising intelligence and ability doesn’t foster self-esteem and lead to accomplishment, but may actually jeopardize success. With the right mindset, we can motivate our kids and help them to raise their grades, as well as reach our own goals–personal and professional. Dweck reveals what all great parents, teachers, CEOs, and athletes already know: how a simple idea about the brain can create a love of learning and a resilience that is the basis of great accomplishment in every area.", "978-0345472328", null, null, "Ballantine Books", "Mindset: The New Psychology of Success", "English" },
                    { new Guid("deca4321-bcac-4116-849f-bbdd74483f87"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "It began with a break-in at the Democratic National Committee headquarters in Washington DC, on 17 June 1972. Bob Woodward, a journalist for the Washington Post, was called into the office on a Saturday morning to cover the story. Carl Bernstein, a political reporter on the Post, was also assigned. They soon learned this was no ordinary burglary. Following lead after lead, Woodward and Bernstein picked up a trail of money, conspiracy and high-level pressure that ultimately led to the doors of the Oval Office. Men very close to the President were implicated, and then Richard Nixon himself. Over a period of months, Woodward met secretly with Deep Throat, for decades the most famous anonymous source in the history of journalism. As he and Bernstein pieced the jigsaw together, they produced a series of explosive stories that would not only win the Post a Pulitzer Prize, they would bring about the President's scandalous downfall. ALL THE PRESIDENT'S MEN documents this amazing story. Taut, gripping and fascinating, it is a classic of its kind -- the true story of the events that changed the American presidency.", "080-7897015427", null, null, "Simon & Schuster UK", "All the President's Men", "English" },
                    { new Guid("52e692a7-28af-40b7-b732-a026ab869b0d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Harry Potter has never even heard of Hogwarts when the letters start dropping on the doormat at number four, Privet Drive. Addressed in green ink on yellowish parchment with a purple seal, they are swiftly confiscated by his grisly aunt and uncle. Then, on Harry’s eleventh birthday, a great beetle-eyed giant of a man called Rubeus Hagrid bursts in with some astonishing news: Harry Potter is a wizard, and he has a place at Hogwarts School of Witchcraft and Wizardry. An incredible adventure is about to begin!", "978-1408855898", null, null, "Bloomsbury Children's Books", "Harry Potter and the Philosopher's Stone", "English" },
                    { new Guid("58275004-6db8-488c-8162-4454335bdf41"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Harry Potter’s summer has included the worst birthday ever, doomy warnings from a house-elf called Dobby, and rescue from the Dursleys by his friend Ron Weasley in a magical flying car! Back at Hogwarts School of Witchcraft and Wizardry for his second year, Harry hears strange whispers echo through empty corridors – and then the attacks start. Students are found as though turned to stone … Dobby’s sinister predictions seem to be coming true.", "978-1408855904", null, null, "Bloomsbury Children's Books", "Harry Potter and the Chamber of Secrets", "English" },
                    { new Guid("1a126438-9879-4b11-970f-e423e03a7ecc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "When the Knight Bus crashes through the darkness and screeches to a halt in front of him, it’s the start of another far from ordinary year at Hogwarts for Harry Potter. Sirius Black, escaped mass-murderer and follower of Lord Voldemort, is on the run – and they say he is coming after Harry. In his first ever Divination class, Professor Trelawney sees an omen of death in Harry’s tea leaves … But perhaps most terrifying of all are the Dementors patrolling the school grounds, with their soul-sucking kiss.", "978-1408855911", null, null, "Bloomsbury Children's Books", "Harry Potter and the Prisoner of Azkaban", "English" },
                    { new Guid("313a997d-8aa5-4998-98e5-32b00fe5c21a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "The Triwizard Tournament is to be held at Hogwarts. Only wizards who are over seventeen are allowed to enter – but that doesn’t stop Harry dreaming that he will win the competition. Then at Hallowe’en, when the Goblet of Fire makes its selection, Harry is amazed to find his name is one of those that the magical cup picks out. He will face death-defying tasks, dragons and Dark wizards, but with the help of his best friends, Ron and Hermione, he might just make it through – alive!", "978-1408855928", null, null, "Bloomsbury Children's Books", "Harry Potter and the Goblet of Fire", "English" },
                    { new Guid("257c09a0-7a94-4e08-8f73-93d64ec0fa56"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dark times have come to Hogwarts. After the Dementors’ attack on his cousin Dudley, Harry Potter knows that Voldemort will stop at nothing to find him. There are many who deny the Dark Lord’s return, but Harry is not alone: a secret order gathers at Grimmauld Place to fight against the Dark forces. Harry must allow Professor Snape to teach him how to protect himself from Voldemort’s savage assaults on his mind. But they are growing stronger by the day and Harry is running out of time.", "978-1408855935", null, null, "Bloomsbury Children's Books", "Harry Potter and the Order of the Phoenix", "English" },
                    { new Guid("54c53b68-4384-4b5b-a0b6-e995fd61f439"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "When Dumbledore arrives at Privet Drive one summer night to collect Harry Potter, his wand hand is blackened and shrivelled, but he does not reveal why. Secrets and suspicion are spreading through the wizarding world, and Hogwarts itself is not safe. Harry is convinced that Malfoy bears the Dark Mark: there is a Death Eater amongst them. Harry will need powerful magic and true friends as he explores Voldemort’s darkest secrets, and Dumbledore prepares him to face his destiny.", "978-1408855942", null, null, "Bloomsbury Children's Books", "Harry Potter and the Half-Blood Prince", "English" },
                    { new Guid("d3e2de6e-9e1e-4d2e-b759-32f7d4263a46"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "As he climbs into the sidecar of Hagrid’s motorbike and takes to the skies, leaving Privet Drive for the last time, Harry Potter knows that Lord Voldemort and the Death Eaters are not far behind. The protective charm that has kept Harry safe until now is now broken, but he cannot keep hiding. The Dark Lord is breathing fear into everything Harry loves, and to stop him Harry will have to find and destroy the remaining Horcruxes. The final battle must begin – Harry must stand and face his enemy.", "978-1408855959", null, null, "Bloomsbury Children's Books", "Harry Potter and the Deathly Hallows", "English" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId", "Created", "CreatedBy", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { new Guid("d12bdef9-84f3-4de9-bdec-940f13971782"), new Guid("1d996191-7d04-498d-91bf-2fb4cfbb1d1a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("54c53b68-4384-4b5b-a0b6-e995fd61f439"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("deca4321-bcac-4116-849f-bbdd74483f87"), new Guid("ef0a6dd8-a451-4891-ae04-7d671ce0cac9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("deca4321-bcac-4116-849f-bbdd74483f87"), new Guid("92d3c08e-335d-4a2e-8e73-ad4f8309146b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("257c09a0-7a94-4e08-8f73-93d64ec0fa56"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("52e692a7-28af-40b7-b732-a026ab869b0d"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("58275004-6db8-488c-8162-4454335bdf41"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("d3e2de6e-9e1e-4d2e-b759-32f7d4263a46"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("1a126438-9879-4b11-970f-e423e03a7ecc"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { new Guid("313a997d-8aa5-4998-98e5-32b00fe5c21a"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "BookId", "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("54c53b68-4384-4b5b-a0b6-e995fd61f439"), 10, "Fantasy Fiction" },
                    { new Guid("257c09a0-7a94-4e08-8f73-93d64ec0fa56"), 9, "Fantasy Fiction" },
                    { new Guid("313a997d-8aa5-4998-98e5-32b00fe5c21a"), 8, "Fantasy Fiction" },
                    { new Guid("58275004-6db8-488c-8162-4454335bdf41"), 6, "Fantasy Fiction" },
                    { new Guid("52e692a7-28af-40b7-b732-a026ab869b0d"), 5, "Fantasy Fiction" },
                    { new Guid("deca4321-bcac-4116-849f-bbdd74483f87"), 4, "Thriller" },
                    { new Guid("deca4321-bcac-4116-849f-bbdd74483f87"), 3, "Politics" },
                    { new Guid("d12bdef9-84f3-4de9-bdec-940f13971782"), 2, "Neuroscience" },
                    { new Guid("d12bdef9-84f3-4de9-bdec-940f13971782"), 1, "Psychology" },
                    { new Guid("1a126438-9879-4b11-970f-e423e03a7ecc"), 7, "Fantasy Fiction" },
                    { new Guid("d3e2de6e-9e1e-4d2e-b759-32f7d4263a46"), 11, "Fantasy Fiction" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("1a126438-9879-4b11-970f-e423e03a7ecc"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("257c09a0-7a94-4e08-8f73-93d64ec0fa56"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("313a997d-8aa5-4998-98e5-32b00fe5c21a"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("52e692a7-28af-40b7-b732-a026ab869b0d"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("54c53b68-4384-4b5b-a0b6-e995fd61f439"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("58275004-6db8-488c-8162-4454335bdf41"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("d12bdef9-84f3-4de9-bdec-940f13971782"), new Guid("1d996191-7d04-498d-91bf-2fb4cfbb1d1a") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("d3e2de6e-9e1e-4d2e-b759-32f7d4263a46"), new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("deca4321-bcac-4116-849f-bbdd74483f87"), new Guid("92d3c08e-335d-4a2e-8e73-ad4f8309146b") });

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "BookId", "AuthorId" },
                keyValues: new object[] { new Guid("deca4321-bcac-4116-849f-bbdd74483f87"), new Guid("ef0a6dd8-a451-4891-ae04-7d671ce0cac9") });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("1a126438-9879-4b11-970f-e423e03a7ecc"), 7 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("257c09a0-7a94-4e08-8f73-93d64ec0fa56"), 9 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("313a997d-8aa5-4998-98e5-32b00fe5c21a"), 8 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("52e692a7-28af-40b7-b732-a026ab869b0d"), 5 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("54c53b68-4384-4b5b-a0b6-e995fd61f439"), 10 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("58275004-6db8-488c-8162-4454335bdf41"), 6 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("d12bdef9-84f3-4de9-bdec-940f13971782"), 1 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("d12bdef9-84f3-4de9-bdec-940f13971782"), 2 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("d3e2de6e-9e1e-4d2e-b759-32f7d4263a46"), 11 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("deca4321-bcac-4116-849f-bbdd74483f87"), 3 });

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumns: new[] { "BookId", "Id" },
                keyValues: new object[] { new Guid("deca4321-bcac-4116-849f-bbdd74483f87"), 4 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("1d996191-7d04-498d-91bf-2fb4cfbb1d1a"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("39b08695-8a86-4e29-a4c5-6d0cd6c66813"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("92d3c08e-335d-4a2e-8e73-ad4f8309146b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("ef0a6dd8-a451-4891-ae04-7d671ce0cac9"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("1a126438-9879-4b11-970f-e423e03a7ecc"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("257c09a0-7a94-4e08-8f73-93d64ec0fa56"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("313a997d-8aa5-4998-98e5-32b00fe5c21a"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("52e692a7-28af-40b7-b732-a026ab869b0d"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("54c53b68-4384-4b5b-a0b6-e995fd61f439"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("58275004-6db8-488c-8162-4454335bdf41"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("d12bdef9-84f3-4de9-bdec-940f13971782"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("d3e2de6e-9e1e-4d2e-b759-32f7d4263a46"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: new Guid("deca4321-bcac-4116-849f-bbdd74483f87"));

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
    }
}
