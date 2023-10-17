using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Musics;
using Darwin.Model.Response.Musics;
using Darwin.Service.Features.Musics.Commands;
using Darwin.Service.Features.Musics.Queries;
using Darwin.Service.UserHelper;
using Mapster;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Linq.Expressions;

namespace Darwin.UnitTests;

public class MusicTests
{
    private readonly IGenericRepository<Music> _musicRepository;
    private readonly IGenericRepository<Mood> _moodRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<ContentAgeRate> _contentAgeRateRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public MusicTests()
    {
        _musicRepository = Substitute.For<IGenericRepository<Music>>();
        _moodRepository = Substitute.For<IGenericRepository<Mood>>();
        _categoryRepository = Substitute.For<IGenericRepository<Category>>();
        _contentAgeRateRepository = Substitute.For<IGenericRepository<ContentAgeRate>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _currentUser = Substitute.For<ICurrentUser>();
    }

    //GetMusics
    [Fact]
    public async Task GetMusicQuery_Should_Success_WhenFoundMusics()
    {
        //Arrange

        var musicList=new List<Music>(){

            new Music(){
                Id=new Guid(),
                Name="Hurt you",
                ImageUrl="hurtyou.img",
                IsUsable=false,
                CreatedAt=DateTime.UtcNow.Ticks,
            },
            new Music(){
                Id=new Guid(),
                Name="Is There Someone Else?",
                ImageUrl="tryMaybeThere.png",
                IsUsable=true,
                CreatedAt=DateTime.UtcNow.Ticks,
            },
            new Music(){
                Id=new Guid(),
                Name="Still Loving you",
                ImageUrl="Scorpions.jpeg",
                IsUsable=false,
                CreatedAt=DateTime.UtcNow.Ticks,
            }
        };

        _musicRepository.GetAllAsync().Returns(Task.FromResult(musicList));
        musicList.Adapt<List<GetMusicResponse>>();
        var query= new GetMusics.Query();
        var queryHandler= new GetMusics.QueryHandler(_musicRepository,_currentUser);
        //Act
        var result= await queryHandler.Handle(query,CancellationToken.None);

        //Assert
        Assert.NotNull(result.Data);
    }


    //DeleteMusic


    [Fact]
    public async Task DeleteMusic_Should_Success_WhenExecuteSuccess()
    {
        var music=new Music()
        {
            Id=new Guid(),
            Name="name",
            ImageUrl="abersie.png",
            IsUsable=!false,
            CreatedAt=DateTime.UtcNow.Ticks
        };

        _musicRepository.GetAsync(Arg.Any<Expression<Func<Music, bool>>>()).Returns(music);
        _musicRepository.RemoveAsync(Arg.Any<Music>()).Returns(Task.FromResult(music));

        var command= new DeleteMusic.Command(music.Id);
        var commandHandler = new DeleteMusic.CommandHandler(_musicRepository,_unitOfWork);
    }


    //GetMusicById

    //////[Fact]
    //////public async Task GetMusicByIdQuery_Should_Success_WhenFoundMusic()
    //////{
    //////    //Arrange

    //////    var musicId= Guid.NewGuid();

    //////    var music=new Music()
    //////    {
    //////        Id = musicId,
    //////        Name = "Still Loving you",
    //////        ImageUrl = "Scorpions.jpeg",
    //////        IsUsable = false,
    //////        CreatedAt = DateTime.UtcNow.Ticks,
    //////    };

    //////    var musicList=new List<Music>(){
    //////         new Music(){
    //////             Id=new Guid(),
    //////             Name="Hurt you",
    //////             ImageUrl="hurtyou.img",
    //////             IsUsable=false,
    //////             CreatedAt=DateTime.UtcNow.Ticks,
    //////         },
    //////        new Music(){
    //////            Id=new Guid(),
    //////            Name="Hurt you",
    //////            ImageUrl="hurtyou.img",
    //////            IsUsable=false,
    //////            CreatedAt=DateTime.UtcNow.Ticks,
    //////        },
    //////        new Music(){
    //////            Id=new Guid(),
    //////            Name="Is There Someone Else?",
    //////            ImageUrl="tryMaybeThere.png",
    //////            IsUsable=true,
    //////            CreatedAt=DateTime.UtcNow.Ticks,
    //////            },
    //////        };
    //////    musicList.Add(music);

    //////    _dbContext.Musics.Include(x => x.Moods).Include(c => c.Categories).SingleOrDefaultAsync(Arg.Any<Expression<Func<Music, bool>>>()).Returns(Task.FromResult(music));
    //////    //_musicRepository.GetAllAsync(Arg.Any<Expression<Func<Music,bool>>>()).Returns(Task.FromResult(musicList));
    //////    musicList.Adapt<GetMusicByIdResponse>();

    //////    var getMusicByResponse=new GetMusicByIdResponse()
    //////    {
    //////        Id=musicId,
    //////        Name=music.Name,
    //////        ImageUrl =music.ImageUrl,
    //////        IsUsable=music.IsUsable
    //////    };
    //////    var query= new GetMusicByIdQuery(musicId);
    //////    var queryHandler= new GetMusicByIdQuery.Handler(_dbContext);
    //////    //Act
    //////    var result= await queryHandler.Handle(query,CancellationToken.None);

    //////    //Assert
    //////    Assert.NotNull(result.Data);
    //////    Assert.True(result.StatusCode == StatusCodes.Status200OK);
    //////}

    // SearchMusic

    [Fact]
    public async Task SearchMusicsQuery_Should_Success_WhenFoundMusics()
    {
        //Arrange

        var musicList=new List<Music>{

            new Music(){
                Id=new Guid(),
                Name="Hurt you",
                ImageUrl="hurtyou.img",
                IsUsable=false,
                CreatedAt=DateTime.UtcNow.Ticks,
            },
            new Music(){
                Id=new Guid(),
                Name="Is There Someone Else?",
                ImageUrl="tryMaybeThere.png",
                IsUsable=true,
                CreatedAt=DateTime.UtcNow.Ticks,
            },
            new Music(){
                Id=new Guid(),
                Name="Still Loving you",
                ImageUrl="Scorpions.jpeg",
                IsUsable=false,
                CreatedAt=DateTime.UtcNow.Ticks,
            },
            new Music(){
                Id=new Guid(),
                Name="Feel it Still",
                ImageUrl="portugal_theman.jpeg",
                IsUsable=false,
                CreatedAt=DateTime.UtcNow.Ticks,
            }
        };
        string searchText="Still";
        _musicRepository.GetAllAsync(Arg.Any<Expression<Func<Music, bool>>>()).Returns(Task.FromResult(musicList));
        musicList.Adapt<List<GetMusicResponse>>();
        var query= new SearchMusics.Query(searchText);
        var queryHandler= new SearchMusics.QueryHandler(_musicRepository);
        //Act
        var result= await queryHandler.Handle(query,CancellationToken.None);

        //Assert
        Assert.NotNull(result.Data);
    }



    //CreateMusic


    //[Fact]
    //public async Task CreateMusicCommand_Should_Success_WhenCreatedMusics()
    //{
    //    //Arrange
    //    var categoryId=new Guid();
    //    var category=new Category()
    //    {
    //        Id=categoryId,
    //        Name="Category",
    //        CreatedAt = DateTime.UtcNow.Ticks,
    //        ImageUrl="dene.jpg",
    //        IsUsable = true
    //    };
    //    var moodId=new Guid();
    //    var mood=new Mood()
    //    {
    //        Id=moodId,
    //        Name="Mood",
    //        CreatedAt = DateTime.UtcNow.Ticks,
    //        ImageUrl="mood.jpg",
    //        IsUsable = true
    //    };

    //    var music= new Music()
    //    {
    //        Id=new Guid(),
    //        Name="Hurt you",
    //        ImageUrl="hurtyou.img",
    //        IsUsable=false,
    //        CreatedAt=DateTime.UtcNow.Ticks,
    //        Categories=new List<Category>()
    //        {
    //            new Category {
    //            Id=categoryId,
    //            Name="Category",
    //            CreatedAt = DateTime.UtcNow.Ticks,
    //            ImageUrl="dene.jpg",
    //            IsUsable = true}
    //        } ,
    //        Moods=new List<Mood>()
    //        {
    //            new Mood(){
    //                Id=moodId,
    //                Name="Mood",
    //                CreatedAt = DateTime.UtcNow.Ticks,
    //                ImageUrl="mood.jpg",
    //                IsUsable = true
    //            }
    //        }

    //    };
    //    var contentAgeRate=new Guid();
    //    var categoryIds=new List<Guid>();
    //    categoryIds.Add(categoryId);
    //    var moodIds=new List<Guid>();
        
    //    moodIds.Add(moodId);


    //    _musicRepository.CreateAsync(music).Returns(music);
    //    var createdMusicResponse=new CreatedMusicResponse()
    //    {
    //        Id=music.Id,
    //        ImageUrl=music.ImageUrl,
    //        IsUsable=music.IsUsable,
    //        Name=music.Name
    //    };
    //    music.Adapt<CreatedMusicResponse>();
    //    var request=new CreateMusicRequest(music.Name,music.ImageUrl,music.IsUsable,categoryIds,moodIds,contentAgeRate);
    //    var command=new CreateMusic.Command(request);
    //    var commandHandler=new CreateMusic.CommandHandler(_musicRepository,_categoryRepository,_moodRepository,_unitOfWork,_contentAgeRateRepository);

    //    //act
    //    var result=await commandHandler.Handle(command,CancellationToken.None);


    //    //assert
    //    Assert.True(result.StatusCode == StatusCodes.Status201Created);
    //    Assert.Equal(result.Data.Name, createdMusicResponse.Name);
    //    Assert.Equal(result.Data.Id, createdMusicResponse.Id);
    //    Assert.Equal(result.Data.IsUsable, createdMusicResponse.IsUsable);
    //    Assert.Equal(result.Data.ImageUrl, createdMusicResponse.ImageUrl);

    //}


}
