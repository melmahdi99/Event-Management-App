using System;
using Domain;
using Microsoft.EntityFrameworkCore;
namespace Persistence;

public class ActivityRepo_Impl : IActivityRepo
{
    private readonly AppDbContext _context;
    public ActivityRepo_Impl(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Activity>> GetActivitiesAsync()
    {
        var activities = await _context.Activities.ToListAsync();
        return activities;
    }

    public async Task<Activity> GetActivityAsync(Guid id)
    {
        var activity = await _context.Activities.FindAsync(id);
        return activity!;
    }

    public async Task<string> CreateActivity(Activity activity)
    {
        var id = _context.Activities.Add(activity);
        await _context.SaveChangesAsync();
        return activity.Id.ToString();
    }
}

