using System;
using Domain;


namespace Application;

public interface IActivityService
{
    // public Task<IEnumerable<ReadActivityDto>> GetActivitiesAsync();
    // public Task<ReadActivityDto> GetActivityAsync(Guid id);
    // public Task<ReadActivityDto> CreateActivity(CreateActivityDto dto);
    public Task<IEnumerable<ReadActivityDto>> GetActivitiesByBuffer(double Lat, double Long, double radius);
    public Task UpdateActivity(FullReadActivityDto dto);
    public Task DeleteActivity(Guid id);
}
