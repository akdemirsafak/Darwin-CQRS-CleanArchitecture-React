using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Linq.Expressions;

namespace Darwin.UnitTests;

public class ContentTests
{
    private readonly IGenericRepository<Content> _contentRepository;

    public ContentTests()
    {
        _contentRepository = Substitute.For<IGenericRepository<Content>>();
    }


    //DeleteMusic


    [Fact]
    public async Task DeleteContent_Should_Success_WhenExecuteSuccess()
    {
        //Assert
        var content=new Content()
        {
            Id=new Guid(),
            Name="name",
            ImageUrl="abersie.png",
            IsUsable=!false,
            CreatedOnUtc = DateTime.UtcNow
        };

        _contentRepository.GetAsync(Arg.Any<Expression<Func<Content, bool>>>()).Returns(content);
        _contentRepository.RemoveAsync(Arg.Any<Content>()).Returns(Task.FromResult(content));

        var command= new DeleteContent.Command(content.Id);
        var commandHandler = new DeleteContent.CommandHandler(_contentRepository);

        //Act
        var result= await commandHandler.Handle(command,CancellationToken.None);

        //Assert
        Assert.True(result.StatusCode == StatusCodes.Status204NoContent);

    }


    //GetContentById

    [Fact]
    public async Task GetContentById_Should_ReturnContentWith200StatusCode()
    {
        var content=new Content()
        {
            Id=new Guid(),
            Name="name",
            ImageUrl="abersie.png",
            IsUsable=!false
        };

        _contentRepository.GetAsync(Arg.Any<Expression<Func<Content, bool>>>()).Returns(Task.FromResult(content));
        var contentResponse=content.Adapt<GetContentByIdResponse>();
        var query= new GetContentById.Query(content.Id);
        var queryHandler = new GetContentById.QueryHandler(_contentRepository);

        //Act
        var result= await queryHandler.Handle(query,CancellationToken.None);

        //Assert
        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.NotNull(result.Data);
        Assert.Equivalent(result.Data, contentResponse);
    }

    // SearchContent

    [Fact]
    public async Task SearchContentQuery_Should_Success_WhenFoundMusics()
    {
        //Arrange

        var contentList=new List<Content>{

            new Content(){
                Id=new Guid(),
                Name="Hurt you",
                ImageUrl="hurtyou.img",
                IsUsable=false,
                CreatedOnUtc = DateTime.UtcNow,
            },
            new Content(){
                Id=new Guid(),
                Name="Is There Someone Else?",
                ImageUrl="tryMaybeThere.png",
                IsUsable=true,
                CreatedOnUtc = DateTime.UtcNow,
            },
            new Content(){
                Id=new Guid(),
                Name="Still Loving you",
                ImageUrl="Scorpions.jpeg",
                IsUsable=false,
                CreatedOnUtc = DateTime.UtcNow,
            },
            new Content(){
                Id=new Guid(),
                Name="Feel it Still",
                ImageUrl="portugal_theman.jpeg",
                IsUsable=false,
                CreatedOnUtc = DateTime.UtcNow,
            }
        };
        string searchText="Still";
        _contentRepository.GetAllAsync(Arg.Any<Expression<Func<Content, bool>>>()).Returns(Task.FromResult(contentList));
        var getcontentListResponse=contentList.Adapt<List<GetContentResponse>>();
        var query= new SearchContents.Query(searchText);
        var queryHandler= new SearchContents.QueryHandler(_contentRepository);
        //Act
        var result= await queryHandler.Handle(query,CancellationToken.None);

        //Assert
        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.NotNull(result.Data);
        Assert.Equivalent(result.Data, getcontentListResponse);
    }


}
