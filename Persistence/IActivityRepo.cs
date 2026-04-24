using System;
using Domain;
namespace Persistence;

public interface IActivityRepo
{
    public Task<IEnumerable<Activity>> GetActivitiesAsync();

    public Task<Activity> GetActivityAsync(Guid id);
    public Task<string> CreateActivity(Activity activity);

}
