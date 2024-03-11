using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Linq.Expressions;

namespace Darwin.UnitTests;

public class MoodTest
{
    private readonly IGenericRepository<Mood> _moodRepository;
    private readonly IFileService _fileService;
    public MoodTest()
    {
        _fileService = Substitute.For<IFileService>();
        _moodRepository = Substitute.For<IGenericRepository<Mood>>();
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
                CreatedOnUtc = DateTime.UtcNow
            },
            new Mood()
            {
                Id=new Guid(),
                Name="Mood 2",
                ImageUrl="sad.png",
                IsUsable=false,
                CreatedOnUtc = DateTime.UtcNow
            }
        };

        _moodRepository.GetAllAsync().Returns(Task.FromResult(moodList));
        var moodListResponse = moodList.Adapt<List<GetMoodResponse>>();

        var command = new GetMoods.Query();
        var commandHandler = new GetMoods.QueryHandler(_moodRepository);

        //Act

        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result.Data);
        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.Equivalent(moodListResponse, result.Data);

    }

    // [Fact]
    // public async Task CreateMood_Should_SuccessWith201StatusCode_WhenCreated()
    // {
    //     //arrange
    //
    //     var mood = new Mood()
    //     {
    //         Id = new Guid(),
    //         Name = "Belirsiz",
    //         ImageUrl = "nothing.jpg",
    //         CreatedOnUtc = DateTime.UtcNow,
    //         IsUsable = false
    //     };
    //
    //     _moodRepository.CreateAsync(Arg.Any<Mood>()).Returns(Task.FromResult(mood));
    //     var createdMoodResponse = mood.Adapt<CreatedMoodResponse>();
    //
    //     var request = new CreateMoodRequest(mood.Name, mood.ImageFile, mood.IsUsable);
    //     var command = new CreateMood.Command(request);
    //     var commandHandler = new CreateMood.CommandHandler(_moodRepository,_fileService);
    //
    //     //action
    //
    //     var result = await commandHandler.Handle(command, CancellationToken.None);
    //     //Assert
    //     Assert.True(result.StatusCode == StatusCodes.Status201Created);
    //     Assert.NotNull(result.Data);
    //     Assert.Equivalent(result.Data, createdMoodResponse);
    // }


    [Fact]
    public async Task UpdateMood_Should_SuccessWith200StatusCode_AndUpdatedMoodResponse_WhenUpdated()
    {
        // Arrange

        var mood = new Mood()
        {
            Id = new Guid(),
            Name = "Hadi",
            ImageUrl = "nothing.jpg",
            CreatedOnUtc = DateTime.UtcNow,
            IsUsable = false
        };
        var newValues = new Mood()
        {
            Id = mood.Id,
            Name = "Yeni deger",
            ImageUrl = "technology.image",
            IsUsable = true,
        };
        _moodRepository.GetAsync(Arg.Any<Expression<Func<Mood, bool>>>()).Returns(Task.FromResult(mood));
        _moodRepository.UpdateAsync(Arg.Any<Mood>()).Returns(Task.FromResult(mood));
        var updatedMoodResponse = newValues.Adapt<UpdatedMoodResponse>();

        var request = new UpdateMoodRequest(newValues.Name, newValues.ImageUrl, newValues.IsUsable);
        var command = new UpdateMood.Command(mood.Id, request);
        var commandHandler = new UpdateMood.CommandHandler(_moodRepository);

        // Action
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.NotNull(result.Data);
        Assert.Equivalent(result.Data, updatedMoodResponse);
    }



}
