using System;
using Domain;
using Persistence;
using AutoMapper;

namespace Application;

public class ActivityService_Impl : IActivityService
{
    private readonly IActivityRepo _activityRepo;
    // private IActivityMapper _mapper;
    private readonly IMapper _mapper;
    //private readonly ILogger<ActivityService_Impl> _logger;
    public ActivityService_Impl(IActivityRepo activityRepo, IMapper mapper)
    {
        _activityRepo = activityRepo;
        _mapper = mapper;
        //_logger = logger;
    }

    // public async Task<IEnumerable<ReadActivityDto>> GetActivitiesAsync(CancellationToken ct)
    // {
    //     var activities = await _activityRepo.GetActivitiesAsync(ct);
    //     return activities.Select(a => _mapper.Map<ReadActivityDto>(a));
    // }

    // public async Task<ReadActivityDto> GetActivityAsync(Guid id)
    // {
    //     var activity = await _activityRepo.GetActivityAsync(id);

    //     //_logger.LogInformation("GET request for activity: {id}, {title}", activity.Id, activity.Title);
    //     return _mapper.Map<ReadActivityDto>(activity);
    // }

    // public async Task<ReadActivityDto> CreateActivity(CreateActivityDto dto)
    // {
    //     // var entity = _mapper.ToEntity(dto);
    //     var entity = _mapper.Map<Domain.Activity>(dto);
    //     var a = await _activityRepo.CreateActivity(entity);
    //     return _mapper.Map<ReadActivityDto>(a);
    // }

    public async Task<IEnumerable<ReadActivityDto>> GetActivitiesByBuffer(double Lat, double Long, double radius)
    {
        var activities = await _activityRepo.GetActivitiesByBuffer(Lat, Long, radius);
        return activities.Select(a => _mapper.Map<ReadActivityDto>(a));
    }

    public async Task DeleteActivity(Guid id)
    {
        await _activityRepo.DeleteActivity(id);
    }

    public async Task UpdateActivity(FullReadActivityDto dto)
    {
        var entity = _mapper.Map<Domain.Activity>(dto);
        await _activityRepo.UpdateActivity(entity);
       
    }
}
