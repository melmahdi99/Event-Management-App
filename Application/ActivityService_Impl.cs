using System;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application;

public class ActivityService_Impl : IActivityService
{
    private readonly IActivityRepo _activityRepo;
    //private readonly ILogger<ActivityService_Impl> _logger;
    public ActivityService_Impl(IActivityRepo activityRepo)
    {
        _activityRepo = activityRepo;
        //_logger = logger;
    }

    public async Task<IEnumerable<Activity>> GetActivitiesAsync()
    {
        var activities = await _activityRepo.GetActivitiesAsync();
        return activities;
    }

    public async Task<Activity> GetActivityAsync(Guid id)
    {
        var activity = await _activityRepo.GetActivityAsync(id);
        //_logger.LogInformation("GET request for activity: {id}, {title}", activity.Id, activity.Title);
        return activity;
    }

    public async Task<string> CreateActivity(Activity activity)
    {
        var id = await _activityRepo.CreateActivity(activity);
        return activity.Id.ToString();
    }
}
