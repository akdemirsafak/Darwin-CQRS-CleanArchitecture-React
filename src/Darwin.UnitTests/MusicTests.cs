using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Response.Musics;
using Darwin.Service.Features.Musics.Commands;
using Darwin.Service.Features.Musics.Queries;
using Darwin.Service.Helper;
using Mapster;
using NSubstitute;
using System.Linq.Expressions;

namespace Darwin.UnitTests;

public class MusicTests
{
    private readonly IGenericRepository<Music> _musicRepository;
    private readonly IGenericRepository<Mood> _moodRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<AgeRate> _ageRateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MusicTests()
    {
        _musicRepository = Substitute.For<IGenericRepository<Music>>();
        _moodRepository = Substitute.For<IGenericRepository<Mood>>();
        _categoryRepository = Substitute.For<IGenericRepository<Category>>();
        _ageRateRepository = Substitute.For<IGenericRepository<AgeRate>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    //GetMusics
    //[Fact]
    //public async Task GetMusicQuery_Should_Success_WhenFoundMusics()
    //{
    //    //Arrange

    //    var musicList=new List<Music>(){

    //        new Music(){
    //            Id=new Guid(),
    //            Name="Hurt you",
    //            ImageUrl="hurtyou.img",
    //            IsUsable=false,
    //            CreatedAt=DateTime.UtcNow.Ticks,
    //        },
    //        new Music(){
    //            Id=new Guid(),
    //            Name="Is There Someone Else?",
    //            ImageUrl="tryMaybeThere.png",
    //            IsUsable=true,
    //            CreatedAt=DateTime.UtcNow.Ticks,
    //        },
    //        new Music(){
    //            Id=new Guid(),
    //            Name="Still Loving you",
    //            ImageUrl="Scorpions.jpeg",
    //            IsUsable=false,
    //            CreatedAt=DateTime.UtcNow.Ticks,
    //        }
    //    };

    //    _musicRepository.GetAllAsync().Returns(Task.FromResult(musicList));
    //    musicList.Adapt<List<GetMusicResponse>>();
    //    var query= new GetMusics.Query();
    //    var queryHandler= new GetMusics.QueryHandler(_musicRepository,_currentUser);
    //    //Act
    //    var result= await queryHandler.Handle(query,CancellationToken.None);

    //    //Assert
    //    Assert.NotNull(result.Data);
    //}


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

}
