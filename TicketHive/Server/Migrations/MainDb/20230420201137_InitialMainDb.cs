using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketHive.Server.Migrations.MainDb
{
    /// <inheritdoc />
    public partial class InitialMainDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    NumberOfTickets = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventModelId = table.Column<int>(type: "int", nullable: true),
                    UserModelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingModel_Events_EventModelId",
                        column: x => x.EventModelId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookingModel_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EndTime", "EventType", "Host", "ImageUrl", "Location", "Name", "NumberOfTickets", "Price", "StartTime" },
                values: new object[,]
                {
                    { 1, "A concert featuring various artists in the local park", new DateTime(2023, 8, 1, 23, 0, 0, 0, DateTimeKind.Unspecified), 1, "The local community council", "image 1.png", "The local park", "Concert in the park", 1000, 350m, new DateTime(2023, 8, 1, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "A new exhibit featuring local artists", new DateTime(2023, 10, 14, 22, 0, 0, 0, DateTimeKind.Unspecified), 2, "The local art museum", "image 2.png", "Center Art Museum", "Art exhibit opening", 500, 100m, new DateTime(2023, 10, 14, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "A wine tasting featuring local wineries", new DateTime(2023, 8, 20, 21, 0, 0, 0, DateTimeKind.Unspecified), 3, "The Wine Association", "image 3.png", "Hillside Wine Garden", "Wine tasting", 300, 200m, new DateTime(2023, 8, 20, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "A charity run to raise funds for local causes", new DateTime(2023, 12, 31, 22, 0, 0, 0, DateTimeKind.Unspecified), 4, "The local sports association", "image 4.png", "Downtown", "Charity run", 2000, 800m, new DateTime(2023, 12, 31, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "A comedy show featuring local comedians", new DateTime(2023, 11, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), 5, "The Comedy Club", "image 5.png", "Central Comedy Club House", "Comedy show", 500, 100m, new DateTime(2023, 11, 15, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "A film festival showcasing international films", new DateTime(2023, 1, 5, 22, 0, 0, 0, DateTimeKind.Unspecified), 6, "The Film Society", "image 6.png", "Film Society Cinema", "Film festival", 120, 300m, new DateTime(2023, 1, 5, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "A music festival featuring international musicians", new DateTime(2023, 9, 11, 19, 0, 0, 0, DateTimeKind.Unspecified), 7, "Music Association", "image 7.png", "The local park", "Local music festival", 5000, 1500m, new DateTime(2023, 9, 11, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "A fair featuring local artisans selling their crafts", new DateTime(2023, 5, 21, 22, 0, 0, 0, DateTimeKind.Unspecified), 8, "Local artisans association", "image 8.png", "Downtown market place", "Artisan fair", 1000, 50m, new DateTime(2023, 5, 21, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "A one day class on theater production of a classic play", new DateTime(2023, 6, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 11, "Educate Theater Association", "image 9.png", "Central library", "Theater production", 100, 1200m, new DateTime(2023, 6, 1, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "A day of family fun featuring various activities for kids and adults", new DateTime(2023, 7, 30, 16, 0, 0, 0, DateTimeKind.Unspecified), 9, "The local community center", "image 10.png", "Community Hall", "Family fun day", 350, 80m, new DateTime(2023, 7, 30, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "A science fair featuring local scientists and their research", new DateTime(2023, 10, 18, 18, 0, 0, 0, DateTimeKind.Unspecified), 10, "The local science museum", "image 11.png", "The Central Museum", "Science fair", 1000, 50m, new DateTime(2023, 10, 18, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "A fashion show featuring various international designers", new DateTime(2023, 12, 15, 22, 0, 0, 0, DateTimeKind.Unspecified), 5, "The Fashion association", "image 12.png", "The Central Mall", "Fashion show", 300, 450m, new DateTime(2023, 12, 15, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "A formal gala dinner featuring gourmet cuisine and live entertainment", new DateTime(2023, 6, 30, 22, 0, 0, 0, DateTimeKind.Unspecified), 3, "Food&Wine Inc", "image 13.png", "Fine Food Restaurant", "Gala dinner", 300, 1500m, new DateTime(2023, 6, 30, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "A lecture series featuring renowned speakers on various topics", new DateTime(2023, 9, 8, 18, 0, 0, 0, DateTimeKind.Unspecified), 2, "The local university", "image 14.png", "Central Library", "Lecture series", 500, 90m, new DateTime(2023, 9, 8, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "A tech meetup featuring local tech entrepreneurs and their startups", new DateTime(2023, 8, 17, 18, 0, 0, 0, DateTimeKind.Unspecified), 12, "Local startup incubator", "image 15.png", "The House of Incubator", "Tech meetup", 50, 900m, new DateTime(2023, 8, 17, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, "A trivia night featuring various categories and fine prizes", new DateTime(2023, 10, 11, 23, 0, 0, 0, DateTimeKind.Unspecified), 13, "The DownTown Pub", "image 16.png", "The DownTown Pub", "Trivia night", 70, 150m, new DateTime(2023, 10, 11, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, "A party featuring international DJ:s and live entertainment", new DateTime(2023, 11, 25, 23, 0, 0, 0, DateTimeKind.Unspecified), 14, "The local nightclub", "image 17.png", "The local nightclub", "DJ party", 400, 250m, new DateTime(2023, 11, 25, 21, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, "An art workshop featuring a local artist teaching a new technique", new DateTime(2023, 12, 9, 16, 0, 0, 0, DateTimeKind.Unspecified), 15, "The local art school", "image 18.png", "The Central School of Art", "Art workshop", 50, 100m, new DateTime(2023, 12, 9, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, "A screening of a new surprise movie with Q&A session with the director", new DateTime(2023, 8, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), 6, "The local movie theatre", "image 19.png", "The DownTown Movie Theater", "Movie screening", 70, 200m, new DateTime(2023, 8, 5, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, "A charity walk to raise funds for a local charity", new DateTime(2023, 10, 20, 22, 0, 0, 0, DateTimeKind.Unspecified), 4, "Local community organizatio", "image 20.png", "Central town", "Charity walk", 1500, 0m, new DateTime(2023, 10, 20, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, "A night of stand-up comedy featuring local comedians", new DateTime(2023, 7, 28, 22, 0, 0, 0, DateTimeKind.Unspecified), 5, "The HaveFun Comedy Club", "image 21.png", "The local comedy club", "Comedy night", 200, 250m, new DateTime(2023, 7, 28, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, "Learn the fundamentals of how pottery is made", new DateTime(2023, 5, 5, 22, 0, 0, 0, DateTimeKind.Unspecified), 15, "The Pottery Arts Association", "image 22.png", "The local pottery", "Pottery class", 50, 300m, new DateTime(2023, 5, 5, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, "A fitness class featuring a local instructor", new DateTime(2023, 10, 12, 20, 0, 0, 0, DateTimeKind.Unspecified), 16, "The local gym", "image 23.png", "The local park", "Fitness class", 80, 100m, new DateTime(2023, 10, 12, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, "A tech workshop were you get to build your own miniature robot", new DateTime(2023, 8, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), 15, "International Robot Inc", "image 24.png", "The local university", "Build your own robot", 1000, 1500m, new DateTime(2023, 8, 16, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, "An unplugged concert featuring various artists in the local park", new DateTime(2023, 11, 5, 22, 0, 0, 0, DateTimeKind.Unspecified), 1, "The local community council", "image 25.png", "The local park", "Unplugged concert", 1000, 350m, new DateTime(2023, 11, 5, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, "An art exhibition featuring international works of art", new DateTime(2023, 9, 17, 21, 0, 0, 0, DateTimeKind.Unspecified), 2, "Fine Arts Association", "image 26.png", "The local arts gallery", "Contemporary Art Show", 200, 300m, new DateTime(2023, 9, 17, 17, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingModel_EventModelId",
                table: "BookingModel",
                column: "EventModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingModel_UserModelId",
                table: "BookingModel",
                column: "UserModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingModel");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
