using Microsoft.EntityFrameworkCore;
using TicketHive.Shared.Enums;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        // Define tables in the database 
        public DbSet<UserModel> Users { get; set; }
        public DbSet<EventModel> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Price decimal data to be stored as decimal(8, 2) in the database 
            modelBuilder.Entity<EventModel>()
                .Property(e => e.Price)
                .HasColumnType("decimal(8, 2)");

            modelBuilder.Entity<UserModel>()
                .Property(e => e.Country)
                .HasConversion<string>();

            modelBuilder.Entity<EventModel>().HasData(
                new EventModel()
                {
                    Id = 1,
                    Name = "Concert in the park",
                    EventType = EventType.Concert,
                    NumberOfTickets = 1000,
                    Description = "A concert featuring various artists in the local park",
                    Price = 350,
                    StartTime = new DateTime(2023, 8, 1, 20, 0, 0),
                    EndTime = new DateTime(2023, 8, 1, 23, 0, 0),
                    Location = "The local park",
                    Host = "The local community council",
                    ImageUrl = "image 1.png"
                },
                new EventModel()
                {
                    Id = 2,
                    Name = "Art exhibit opening",
                    EventType = EventType.Exhibition,
                    NumberOfTickets = 500,
                    Description = "A new exhibit featuring local artists",
                    Price = 100,
                    StartTime = new DateTime(2023, 10, 14, 19, 0, 0),
                    EndTime = new DateTime(2023, 10, 14, 22, 0, 0),
                    Location = "Center Art Museum",
                    Host = "The local art museum",
                    ImageUrl = "image 2.png"
                },
                new EventModel()
                {
                    Id = 3,
                    Name = "Wine tasting",
                    EventType = EventType.Tasting,
                    NumberOfTickets = 300,
                    Description = "A wine tasting featuring local wineries",
                    Price = 200,
                    StartTime = new DateTime(2023, 8, 20, 18, 0, 0),
                    EndTime = new DateTime(2023, 8, 20, 21, 0, 0),
                    Location = "Hillside Wine Garden",
                    Host = "The Wine Association",
                    ImageUrl = "image 3.png"
                },
                new EventModel()
                {
                    Id = 4,
                    Name = "Charity run",
                    EventType = EventType.Fundraiser,
                    NumberOfTickets = 2000,
                    Description = "A charity run to raise funds for local causes",
                    Price = 800,
                    StartTime = new DateTime(2023, 12, 31, 18, 0, 0),
                    EndTime = new DateTime(2023, 12, 31, 22, 0, 0),
                    Location = "Downtown",
                    Host = "The local sports association",
                    ImageUrl = "image 4.png"
                },
                new EventModel()
                {
                    Id = 5,
                    Name = "Comedy show",
                    EventType = EventType.Show,
                    NumberOfTickets = 500,
                    Description = "A comedy show featuring local comedians",
                    Price = 100,
                    StartTime = new DateTime(2023, 11, 15, 20, 0, 0),
                    EndTime = new DateTime(2023, 11, 15, 21, 0, 0),
                    Location = "Central Comedy Club House",
                    Host = "The Comedy Club",
                    ImageUrl = "image 5.png"
                },
                new EventModel()
                {
                    Id = 6,
                    Name = "Film festival",
                    EventType = EventType.Cinema,
                    NumberOfTickets = 120,
                    Description = "A film festival showcasing international films",
                    Price = 300,
                    StartTime = new DateTime(2023, 1, 5, 17, 0, 0),
                    EndTime = new DateTime(2023, 1, 5, 22, 0, 0),
                    Location = "Film Society Cinema",
                    Host = "The Film Society",
                    ImageUrl = "image 6.png"
                },
                new EventModel()
                {
                    Id = 7,
                    Name = "Local music festival",
                    EventType = EventType.Festival,
                    NumberOfTickets = 5000,
                    Description = "A music festival featuring international musicians",
                    Price = 1500,
                    StartTime = new DateTime(2023, 9, 11, 13, 0, 0),
                    EndTime = new DateTime(2023, 9, 11, 19, 0, 0),
                    Location = "The local park",
                    Host = "Music Association",
                    ImageUrl = "image 7.png"
                },
                new EventModel()
                {
                    Id = 8,
                    Name = "Artisan fair",
                    EventType = EventType.Market,
                    NumberOfTickets = 1000,
                    Description = "A fair featuring local artisans selling their crafts",
                    Price = 50,
                    StartTime = new DateTime(2023, 5, 21, 18, 0, 0),
                    EndTime = new DateTime(2023, 5, 21, 22, 0, 0),
                    Location = "Downtown market place",
                    Host = "Local artisans association",
                    ImageUrl = "image 8.png"
                },
                new EventModel()
                {
                    Id = 9,
                    Name = "Theater production",
                    EventType = EventType.Lecture,
                    NumberOfTickets = 100,
                    Description = "A one day class on theater production of a classic play",
                    Price = 1200,
                    StartTime = new DateTime(2023, 6, 1, 11, 0, 0),
                    EndTime = new DateTime(2023, 6, 1, 13, 0, 0),
                    Location = "Central library",
                    Host = "Educate Theater Association",
                    ImageUrl = "image 9.png"
                },
                new EventModel()
                {
                    Id = 10,
                    Name = "Family fun day",
                    EventType = EventType.Family,
                    NumberOfTickets = 350,
                    Description = "A day of family fun featuring various activities for kids and adults",
                    Price = 80,
                    StartTime = new DateTime(2023, 7, 30, 9, 0, 0),
                    EndTime = new DateTime(2023, 7, 30, 16, 0, 0),
                    Location = "Community Hall",
                    Host = "The local community center",
                    ImageUrl = "image 10.png"
                },
                new EventModel()
                {
                    Id = 11,
                    Name = "Science fair",
                    EventType = EventType.Fair,
                    NumberOfTickets = 1000,
                    Description = "A science fair featuring local scientists and their research",
                    Price = 50,
                    StartTime = new DateTime(2023, 10, 18, 9, 0, 0),
                    EndTime = new DateTime(2023, 10, 18, 18, 0, 0),
                    Location = "The Central Museum",
                    Host = "The local science museum",
                    ImageUrl = "image 11.png"
                },
                new EventModel()
                {
                    Id = 12,
                    Name = "Fashion show",
                    EventType = EventType.Show,
                    NumberOfTickets = 300,
                    Description = "A fashion show featuring various international designers",
                    Price = 450,
                    StartTime = new DateTime(2023, 12, 15, 18, 0, 0),
                    EndTime = new DateTime(2023, 12, 15, 22, 0, 0),
                    Location = "The Central Mall",
                    Host = "The Fashion association",
                    ImageUrl = "image 12.png"
                },
                new EventModel()
                {
                    Id = 13,
                    Name = "Gala dinner",
                    EventType = EventType.Tasting,
                    NumberOfTickets = 300,
                    Description = "A formal gala dinner featuring gourmet cuisine and live entertainment",
                    Price = 1500,
                    StartTime = new DateTime(2023, 6, 30, 20, 0, 0),
                    EndTime = new DateTime(2023, 6, 30, 22, 0, 0),
                    Location = "Fine Food Restaurant",
                    Host = "Food&Wine Inc",
                    ImageUrl = "image 13.png"
                },
                new EventModel()
                {
                    Id = 14,
                    Name = "Lecture series",
                    EventType = EventType.Exhibition,
                    NumberOfTickets = 500,
                    Description = "A lecture series featuring renowned speakers on various topics",
                    Price = 90,
                    StartTime = new DateTime(2023, 9, 8, 13, 0, 0),
                    EndTime = new DateTime(2023, 9, 8, 18, 0, 0),
                    Location = "Central Library",
                    Host = "The local university",
                    ImageUrl = "image 14.png"
                },
                new EventModel()
                {
                    Id = 15,
                    Name = "Tech meetup",
                    EventType = EventType.MeetUp,
                    NumberOfTickets = 50,
                    Description = "A tech meetup featuring local tech entrepreneurs and their startups",
                    Price = 900,
                    StartTime = new DateTime(2023, 8, 17, 13, 0, 0),
                    EndTime = new DateTime(2023, 8, 17, 18, 0, 0),
                    Location = "The House of Incubator",
                    Host = "Local startup incubator",
                    ImageUrl = "image 15.png"
                },
                new EventModel()
                {
                    Id = 16,
                    Name = "Trivia night",
                    EventType = EventType.Quiz,
                    NumberOfTickets = 70,
                    Description = "A trivia night featuring various categories and fine prizes",
                    Price = 150,
                    StartTime = new DateTime(2023, 10, 11, 19, 0, 0),
                    EndTime = new DateTime(2023, 10, 11, 23, 0, 0),
                    Location = "The DownTown Pub",
                    Host = "The DownTown Pub",
                    ImageUrl = "image 16.png"
                },
                new EventModel()
                {
                    Id = 17,
                    Name = "DJ party",
                    EventType = EventType.Party,
                    NumberOfTickets = 400,
                    Description = "A party featuring international DJ:s and live entertainment",
                    Price = 250,
                    StartTime = new DateTime(2023, 11, 25, 21, 0, 0),
                    EndTime = new DateTime(2023, 11, 25, 23, 0, 0),
                    Location = "The local nightclub",
                    Host = "The local nightclub",
                    ImageUrl = "image 17.png"
                },
                new EventModel()
                {
                    Id = 18,
                    Name = "Art workshop",
                    EventType = EventType.Workshop,
                    NumberOfTickets = 50,
                    Description = "An art workshop featuring a local artist teaching a new technique",
                    Price = 100,
                    StartTime = new DateTime(2023, 12, 9, 13, 0, 0),
                    EndTime = new DateTime(2023, 12, 9, 16, 0, 0),
                    Location = "The Central School of Art",
                    Host = "The local art school",
                    ImageUrl = "image 18.png"
                },
                new EventModel()
                {
                    Id = 19,
                    Name = "Movie screening",
                    EventType = EventType.Cinema,
                    NumberOfTickets = 70,
                    Description = "A screening of a new surprise movie with Q&A session with the director",
                    Price = 200,
                    StartTime = new DateTime(2023, 8, 5, 17, 0, 0),
                    EndTime = new DateTime(2023, 8, 5, 20, 0, 0),
                    Location = "The DownTown Movie Theater",
                    Host = "The local movie theatre",
                    ImageUrl = "image 19.png"
                },
                new EventModel()
                {
                    Id = 20,
                    Name = "Charity walk",
                    EventType = EventType.Fundraiser,
                    NumberOfTickets = 1500,
                    Description = "A charity walk to raise funds for a local charity",
                    Price = 0,
                    StartTime = new DateTime(2023, 10, 20, 18, 0, 0),
                    EndTime = new DateTime(2023, 10, 20, 22, 0, 0),
                    Location = "Central town",
                    Host = "Local community organizatio",
                    ImageUrl = "image 20.png"
                },
                new EventModel()
                {
                    Id = 21,
                    Name = "Comedy night",
                    EventType = EventType.Show,
                    NumberOfTickets = 200,
                    Description = "A night of stand-up comedy featuring local comedians",
                    Price = 250,
                    StartTime = new DateTime(2023, 7, 28, 18, 0, 0),
                    EndTime = new DateTime(2023, 7, 28, 22, 0, 0),
                    Location = "The local comedy club",
                    Host = "The HaveFun Comedy Club",
                    ImageUrl = "image 21.png"
                },
                new EventModel()
                {
                    Id = 22,
                    Name = "Pottery class",
                    EventType = EventType.Workshop,
                    NumberOfTickets = 50,
                    Description = "Learn the fundamentals of how pottery is made",
                    Price = 300,
                    StartTime = new DateTime(2023, 5, 5, 18, 0, 0),
                    EndTime = new DateTime(2023, 5, 5, 22, 0, 0),
                    Location = "The local pottery",
                    Host = "The Pottery Arts Association",
                    ImageUrl = "image 22.png"
                },
                new EventModel()
                {
                    Id = 23,
                    Name = "Fitness class",
                    EventType = EventType.Exercize,
                    NumberOfTickets = 80,
                    Description = "A fitness class featuring a local instructor",
                    Price = 100,
                    StartTime = new DateTime(2023, 10, 12, 19, 0, 0),
                    EndTime = new DateTime(2023, 10, 12, 20, 0, 0),
                    Location = "The local park",
                    Host = "The local gym",
                    ImageUrl = "image 23.png"
                },
                new EventModel()
                {
                    Id = 24,
                    Name = "Build your own robot",
                    EventType = EventType.Workshop,
                    NumberOfTickets = 1000,
                    Description = "A tech workshop were you get to build your own miniature robot",
                    Price = 1500,
                    StartTime = new DateTime(2023, 8, 16, 13, 0, 0),
                    EndTime = new DateTime(2023, 8, 16, 17, 0, 0),
                    Location = "The local university",
                    Host = "International Robot Inc",
                    ImageUrl = "image 24.png"
                },
                new EventModel()
                {
                    Id = 25,
                    Name = "Unplugged concert",
                    EventType = EventType.Concert,
                    NumberOfTickets = 1000,
                    Description = "An unplugged concert featuring various artists in the local park",
                    Price = 350,
                    StartTime = new DateTime(2023, 11, 5, 18, 0, 0),
                    EndTime = new DateTime(2023, 11, 5, 22, 0, 0),
                    Location = "The local park",
                    Host = "The local community council",
                    ImageUrl = "image 25.png"
                },
                new EventModel()
                {
                    Id = 26,
                    Name = "Contemporary Art Show",
                    EventType = EventType.Exhibition,
                    NumberOfTickets = 200,
                    Description = "An art exhibition featuring international works of art",
                    Price = 300,
                    StartTime = new DateTime(2023, 9, 17, 17, 0, 0),
                    EndTime = new DateTime(2023, 9, 17, 21, 0, 0),
                    Location = "The local arts gallery",
                    Host = "Fine Arts Association",
                    ImageUrl = "image 26.png"
                });
        }
    }
}
