using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.PlayLists;
using Darwin.Model.Response.PlayLists;
using Darwin.Service.Features.PlayLists.Commands;
using Darwin.Service.Features.PlayLists.Queries;
using Darwin.Service.Helper;
using Mapster;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Linq.Expressions;

namespace Darwin.UnitTests;

public class PlayListTests
{
    private readonly IGenericRepository <PlayList> _playListRepository;
    private readonly IGenericRepository<Content> _contentRepository;
    private readonly ICurrentUser _currentUser;
    public PlayListTests()
    {
        _playListRepository = Substitute.For<IGenericRepository<PlayList>>();
        _contentRepository = Substitute.For<IGenericRepository<Content>>();
        _currentUser = Substitute.For<ICurrentUser>();

    }

    [Fact]
    public async Task GetPlayLists_Should_Return200WithDatas()
    {

        //Arrange

        var playList = new PlayList()
        {
            Id = Guid.NewGuid(),
            Name = "FirstPlayList",
            IsUsable = true,
            IsPublic = true,
            Description = "description"
        };
        var playList2 = new PlayList()
        {
            Id = Guid.NewGuid(),
            Name = "SecondPlayList",
            IsUsable = true,
            IsPublic = false,
            Description = "description"
        };

        var playLists = new List<PlayList>()
        {
            playList,playList2
        };
        _playListRepository.GetAllAsync().Returns(Task.FromResult(playLists));
        var playListsResponse = playLists.Adapt<List<GetPlayListResponse>>();

        var query = new GetPlayLists.Query();
        var queryHandler = new GetPlayLists.QueryHandler(_playListRepository);



        //Act

        var result = await queryHandler.Handle(query, CancellationToken.None);

        //Assert

        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.NotNull(result.Data);
        Assert.Equivalent(playListsResponse, result.Data);
    }

    //[Fact]
    //public async Task GetPlayListById_Should_Return200WithData()
    //{

    //    /////-----Arrange-----

    //    //Contents
    //    var content = new Content()
    //    {
    //        Id = Guid.NewGuid(),
    //        Name = "Six feet under",
    //        Lyrics = "weeknd",
    //        IsUsable = true,
    //        ImageUrl = "darwinimg.png"
    //    };
    //    var content2 = new Content()
    //    {
    //        Id = Guid.NewGuid(),
    //        Name = "Still loving you",
    //        Lyrics = "deneme1234",
    //        IsUsable = false,
    //        ImageUrl = "scorpions.png"
    //    };
    //    var content3 = new Content()
    //    {
    //        Id = Guid.NewGuid(),
    //        Name = "Back to Black",
    //        Lyrics = "and my tears dry",
    //        IsUsable = true,
    //        ImageUrl = "darwinimg.png"
    //    };

    //    //PlayLists
    //    var playList = new PlayList()
    //    {
    //        Id = Guid.NewGuid(),
    //        Name = "FirstPlayList",
    //        IsUsable = true,
    //        IsPublic = true,
    //        Description = "description",
    //        Contents = new List<Content> { content,content2, content3 }
    //    };
    //    //var playList2 = new PlayList()
    //    //{
    //    //    Id = Guid.NewGuid(),
    //    //    Name = "SecondPlayList",
    //    //    IsUsable = true,
    //    //    IsPublic = false,
    //    //    Description = "description",
    //    //    Contents = new List<Content> { content }
    //    //};

    //    //var playLists = new List<PlayList>()
    //    //{
    //    //    playList,playList2
    //    //};

    //    _playListRepository.GetAsync(x => x.Id == playList.Id).Returns(Task.FromResult(playList));
    //    var playListResponse = playList.Adapt<GetPlayListByIdResponse>();
    //    playListResponse.Contents = playList.Contents.Adapt<List<GetContentResponse>>();

    //    var query = new GetPlayListById.Query(playList.Id);
    //    var queryHandler = new GetPlayListById.QueryHandler(_playListRepository);

    //    /////-----Act-----
    //    var result = await queryHandler.Handle(query, CancellationToken.None);

    //    /////-----Assert-----

    //    Assert.True(result.StatusCode == StatusCodes.Status200OK);
    //    Assert.NotNull(result.Data);
    //    Assert.Equivalent(playListResponse, result.Data);


    //}

    [Fact]
    public async Task CreatePlayList_Should_Return_201WithCreatedPlayList()
    {
        var playList = new CreatePlayListRequest("PlayListName", "Açıklama", true, true);
        var entity = playList.Adapt<PlayList>();
        _playListRepository.CreateAsync(entity).Returns(Task.FromResult(entity));

        var createdPlayListResponse = entity.Adapt<CreatedPlayListResponse>();

        var command = new CreatePlayList.Command(playList,_currentUser.GetUserId);
        var commandHandler = new CreatePlayList.CommandHandler(_playListRepository);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert

        Assert.True(result.StatusCode == StatusCodes.Status201Created);
        Assert.Equivalent(result.Data, createdPlayListResponse);

    }



    [Fact]
    public async Task UpdatePlayList_Should_Return_200WithUpdatedPlayList()
    {
        var playList = new PlayList()
        {
            Id = Guid.NewGuid(),
            Name = "MyPlayList",
            Description = "Old Description",
            IsPublic = true,
            IsUsable = false
        };
        var playListNewValues = new PlayList()
        {
            Id = playList.Id,
            Name = "Yeni playList Adı",
            Description = "Yeni bir açıklama",
            IsPublic = true,
            IsUsable = true
        };


        _playListRepository.GetAsync(Arg.Any<Expression<Func<PlayList, bool>>>()).Returns(playList);

        _playListRepository.UpdateAsync(Arg.Any<PlayList>()).Returns(Task.FromResult(playList));

        var updatedPlayListResponse = playListNewValues.Adapt<UpdatedPlayListResponse>();

        var playListRequest = new UpdatePlayListRequest(playListNewValues.Name, playListNewValues.Description, playListNewValues.IsPublic, playListNewValues.IsUsable);
        var command = new UpdatePlayList.Command(playList.Id, playListRequest,_currentUser.GetUserId);
        var commandHandler = new UpdatePlayList.CommandHandler(_playListRepository);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert

        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.Equivalent(result.Data, updatedPlayListResponse);

    }



    [Fact]
    public async Task DeletePlayList_Should_Return_204WithNoContent()
    {
        var playList = new PlayList()
        {
            Id = Guid.NewGuid(),
            Name = "PlayList1",
            Description = "description",
            IsPublic = true,
            IsUsable = true
        };

        _playListRepository.GetAsync(Arg.Any<Expression<Func<PlayList, bool>>>()).Returns(Task.FromResult(playList));
        playList.IsUsable = false;
        _playListRepository.UpdateAsync(Arg.Any<PlayList>()).Returns(Task.FromResult(playList));

        var command = new DeletePlayList.Command(playList.Id);
        var commandHandler = new DeletePlayList.CommandHandler(_playListRepository);

        //Act

        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.StatusCode == StatusCodes.Status204NoContent);

    }
}
