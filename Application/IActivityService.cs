using System;
using Domain;


namespace Application;

public interface IActivityService
{
    public Task<IEnumerable<Activity>> GetActivitiesAsync();
    public Task<Activity> GetActivityAsync(Guid id);
    public Task<string> CreateActivity(Activity activity);
}
