using System;
using System.Net;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
namespace Persistence;

public class ActivityRepo_Impl : IActivityRepo
{
    private readonly AppDbContext _context;
    public ActivityRepo_Impl(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Activity>> GetActivitiesAsync(CancellationToken ct)
    {
        var activities = await _context.Activities.AsNoTracking().ToListAsync(ct);
        return activities;
    }

    public async Task<Activity> GetActivityAsync(Guid id, CancellationToken ct)
    {
        var activity = await _context.Activities.FindAsync(id);
        return activity!;
    }

    public async Task<Activity> CreateActivity(Activity activity, CancellationToken ct)
    {
        var id = _context.Activities.Add(activity);
        await _context.SaveChangesAsync(ct);
        return activity;
    }

    public async Task<IEnumerable<Activity>> GetActivitiesByBuffer(double Lat, double Long, double radius)
    {
        double R = 6371; 
        var activities = await _context.Activities
        .Where(l => (R * Math.Acos(
            Math.Sin(Lat * Math.PI / 180) * Math.Sin(l.Latitude * Math.PI / 180) +
            Math.Cos(Lat * Math.PI / 180) * Math.Cos(l.Latitude * Math.PI / 180) *
            Math.Cos((l.Longitude * Math.PI / 180) - (Long * Math.PI / 180))
        )) <= radius)
        .ToListAsync();
        return activities;
    }

    public async Task UpdateActivity(Activity activity)
    {
        var a = (await _context.Activities.FindAsync(activity.Id))!;
        a.Category = activity.Category;
        a.City = activity.City;
        a.Date = activity.Date;
        a.Title = activity.Title;
        a.Description = activity.Description;
        a.IsCancelled = activity.IsCancelled;
        a.Venue = activity.Venue;
        a.Latitude = activity.Latitude;
        a.Longitude = activity.Longitude;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteActivity(Guid id)
    {
        await _context.Activities.Where(a => a.Id == id).ExecuteDeleteAsync();
    }
}

