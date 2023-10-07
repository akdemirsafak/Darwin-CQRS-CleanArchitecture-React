﻿using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Moods;
using Darwin.Model.Response.Moods;
using Darwin.Service.Features.Moods.Commands;
using Darwin.Service.Features.Moods.Queries;
using Mapster;
using NSubstitute;
using System.Linq.Expressions;

namespace Darwin.UnitTests;

public class MoodTest
{
    private readonly IGenericRepository<Mood> _moodRepository;
    private readonly IUnitOfWork _unitOfWork;
    public MoodTest()
    {
        _moodRepository = Substitute.For<IGenericRepository<Mood>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
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

        var command= new GetMoods.Query();
        var commandHandler= new GetMoods.QueryHandler(_moodRepository);

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
        var command= new CreateMood.Command(request);
        var commandHandler= new CreateMood.CommandHandler(_moodRepository,_unitOfWork);

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
        _moodRepository.GetAsync(Arg.Any<Expression<Func<Mood, bool>>>()).Returns(Task.FromResult(mood));
        _moodRepository.UpdateAsync(Arg.Any<Mood>()).Returns(Task.FromResult(mood));
        mood.Adapt<UpdatedMoodResponse>();

        UpdatedMoodResponse updatedMoodResponse=new UpdatedMoodResponse()
        {
            Id=newValues.Id,
            IsUsable=newValues.IsUsable,
            Name=newValues.Name
        };
        var request= new UpdateMoodRequest(newValues.Name,newValues.ImageUrl,newValues.IsUsable);
        var command= new UpdateMood.Command(mood.Id,request);
        var commandHandler=new UpdateMood.CommandHandler(_moodRepository, _unitOfWork);

        // Action
        var result= await commandHandler.Handle(command,CancellationToken.None);

        // Assert
        Assert.True(result.StatusCode == 204);
        Assert.NotNull(result.Data);
        //Assert.Equal(result.Data, updatedMoodResponse);
    }



}
