using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Service.Moods.Commands.Create;
using Darwin.Service.Moods.Commands.Update;
using Darwin.Service.Moods.Queries;
using Mapster;
using NSubstitute;
using System.Linq.Expressions;

namespace Darwin.UnitTests;

public class MoodTest
{
    private readonly IGenericRepositoryAsync<Mood> _moodRepository;
    public MoodTest()
    {
        _moodRepository = Substitute.For<IGenericRepositoryAsync<Mood>>();
    }


    [Fact]
    public async Task GetMoods_Should_Success_WhenFound()
    {
        //Arrange
        List<Mood> moodList = new()
        {
            new Mood()
            {
                Id=new Guid(),
                Name="Mood1",
                ImageUrl="moodTest.png",
                IsUsable=true,
                CreatedAt=DateTime.UtcNow.Ticks
            },
            new Mood()
            {
                Id=new Guid(),
                Name="Mood 2",
                ImageUrl="sad.png",
                IsUsable=false,
                CreatedAt=DateTime.UtcNow.Ticks
            }
        };

        _moodRepository.GetAllAsync().Returns(Task.FromResult(moodList));
        moodList.Adapt<List<GetMoodResponse>>();

        var command= new GetMoodsQuery();
        var commandHandler= new GetMoodsQuery.Handler(_moodRepository);

        //Act

        var result=await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result.Data);

    }

    [Fact]
    public async Task CreateMood_Should_Success_WhenCreated()
    {
        //arrange

        var mood=new Mood()
        {
            Id=new Guid(),
            Name="Belirsiz",
            ImageUrl="nothing.jpg",
            CreatedAt = DateTime.UtcNow.Ticks,
            IsUsable=false
        };

        _moodRepository.CreateAsync(Arg.Any<Mood>()).Returns(Task.FromResult(mood));
        mood.Adapt<CreatedMoodResponse>();

        CreatedMoodResponse createdMoodResponse=new CreatedMoodResponse()
        {
            Id=mood.Id,
            IsUsable=mood.IsUsable,
            Name=mood.Name
        };
        var request= new CreateMoodRequest(mood.Name,mood.ImageUrl,mood.IsUsable);
        var command= new CreateMoodCommand(request);
        var commandHandler= new CreateMoodCommand.Handler(_moodRepository);

        //action

        var result= await commandHandler.Handle(command, CancellationToken.None);
        //Assert

        Assert.NotNull(result.Data);
        Assert.True(result.StatusCode == 201);
    }


    [Fact]
    public async Task UpdateMood_Should_Success_WhenUpdated()
    {
        // Arrange

        var mood=new Mood()
        {
            Id=new Guid(),
            Name="Belirsiz",
            ImageUrl="nothing.jpg",
            CreatedAt = DateTime.UtcNow.Ticks,
            IsUsable=false
        };
        var newValues=new Mood()
        {
            Id=mood.Id,
            Name="New Value",
            ImageUrl="technology.image",
            IsUsable=true,
        };
        _moodRepository.GetAsync(Arg.Any<Expression<Func<Mood,bool>>>()).Returns(Task.FromResult(mood));
        _moodRepository.UpdateAsync(Arg.Any<Mood>()).Returns(Task.FromResult(mood));
        mood.Adapt<UpdatedMoodResponse>();

        UpdatedMoodResponse updatedMoodResponse=new UpdatedMoodResponse()
        {
            Id=newValues.Id,
            IsUsable=newValues.IsUsable,
            Name=newValues.Name
        };
        var request= new UpdateMoodRequest(newValues.Name,newValues.ImageUrl,newValues.IsUsable);
        var command= new UpdateMoodCommand(mood.Id,request);
        var commandHandler=new UpdateMoodCommand.Handler(_moodRepository);

        // Action
        var result= await commandHandler.Handle(command,CancellationToken.None);

        // Assert
        Assert.True(result.StatusCode == 204);
        Assert.NotNull(result.Data);
        //Assert.Equal(result.Data, updatedMoodResponse);
    }



}
