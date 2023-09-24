using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Model.Response.Musics;
using Darwin.Service.Musics.Queries;
using Mapster;
using NSubstitute;

namespace Darwin.UnitTests;

public class MusicTests
{
    private readonly IGenericRepositoryAsync<Music> _musicRepository;
    public MusicTests()
    {
        _musicRepository=Substitute.For<IGenericRepositoryAsync<Music>>();
    }
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

        _musicRepository.GetAllAsync().Returns(musicList);
        musicList.Adapt<List<GetMusicResponse>>();
        var query= new GetMusicsQuery();
        var queryHandler= new GetMusicsQuery.Handler(_musicRepository);
        //Act
        var result= await queryHandler.Handle(query,CancellationToken.None);

        //Assert
        Assert.NotNull(result.Data);
    }
}
