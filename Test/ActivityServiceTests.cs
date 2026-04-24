using System;
using Domain;
using Moq;
using Persistence;
using Application;
using FluentAssertions;
namespace Test;

public class ActivityServiceTests
{
    [Fact]
    //Unit testing always happens in 3 steps. 
    public async Task GetActivitiesAsync_ReturnsListOfActivities()
    {
        //Arrange
        //This step sets up the data for the test.
        var mockRepo = new Mock<IActivityRepo>();
        var expectedActivities = new List<Activity>
        {
            new Activity{ Title = "Test Activity 1", Description = "Desc 1", Category = "Cat 1", City = "City 1", Venue = "Venue 1"},
            new Activity{ Title = "Test Activity 2", Description = "Desc 2", Category = "Cat 2", City = "City 2", Venue = "Venue 2"}
    
        };
        mockRepo.Setup(repo => repo.GetActivitiesAsync()).ReturnsAsync(expectedActivities);
        var service = new ActivityService_Impl(mockRepo.Object);

        //Act
        //This step calls the method we are testing with our mock test data
        var results = await service.GetActivitiesAsync();

        //Assert
        //This step ensure that the output from our act step is as expected
        //Typically not enough to do an assertion alone--they test output/data
        //but verification tests behavior
        Assert.Equal(expectedActivities, results);
        //expectedActivities.Should().BeEqualTo(results);
        mockRepo.Verify(repo => repo.GetActivitiesAsync(), Times.Once);
    }

}
