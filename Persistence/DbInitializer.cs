using System;
using Domain;

namespace Persistence;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context)
    {
        if(context.Activities.Any()) return;

        var random = new Random();
        var activities = new List<Activity>();
        var categories = new List<string>
        {
            "Music", "Sports", "Food", "Tech", "Art"
        };
        var cities = new List<string>
        {
            "Little Rock", "Cabot", "Conway", "Jacksonville", "Memphis"
        };
        var venues = new List<string>
        {
            "Main Hall", "City Park", "Conference Center", "Downtown Arena"
        };

        for(int i = 0; i < 20; i++)
        {
            var activity = new Activity
            {
                Title = $"Test Activity {i+1}",
                Description = $"This is test activity number {i+1}",
                Date = DateTimeOffset.Now.AddDays(random.Next(-10, 30)),
                Category = categories[random.Next(categories.Count)],
                IsCancelled = random.NextDouble() < 0.2, 
                Venue = venues[random.Next(venues.Count)],
                City = cities[random.Next(cities.Count)],
                Latitude = random.NextDouble()*180 - 90,
                Longitude = random.NextDouble()*360 - 180
            };
            activities.Add(activity);
        }
        context.Activities.AddRange(activities);
        await context.SaveChangesAsync();
    }
}
