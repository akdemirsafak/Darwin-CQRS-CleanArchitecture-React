using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Categories;
using Darwin.Model.Response.Categories;
using Darwin.Service.Features.Categories.Commands;
using Darwin.Service.Features.Categories.Queries;
using Mapster;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Linq.Expressions;

namespace Darwin.UnitTests;

public class CategoryTests
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CategoryTests()
    {
        _categoryRepository = Substitute.For<IGenericRepository<Category>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task GetCategories_Should_WhenFound()
    {
        //Arrange
        List<Category> categoryList = new()
        {
            new Category()
            {
                Id=new Guid(),
                Name="CategoryTest",
                ImageUrl="categorytest.png",
                IsUsable=true,
                CreatedAt=DateTime.UtcNow.Ticks
            },
            new Category()
            {
                Id=new Guid(),
                Name="Test2",
                ImageUrl="bilmemne.png",
                IsUsable=false,
                CreatedAt=DateTime.UtcNow.Ticks
            }

        };


        _categoryRepository.GetAllAsync().Returns(Task.FromResult(categoryList));
        categoryList.Adapt<List<GetCategoryResponse>>();

        var query= new GetCategories.Query();
        var queryHandler= new GetCategories.QueryHandler(_categoryRepository);

        //Act
        var result=await queryHandler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result.Data);

    }



    [Fact]
    public async Task GetCategoryById_Should_WhenFound()
    {
        var categoryId=new Guid();
        var category1= new Category()
        {
            Id=categoryId,
            Name="CategoryTest",
            ImageUrl="categorytest.png",
            IsUsable=true,
            CreatedAt=DateTime.UtcNow.Ticks
        };
        var category2= new Category()
        {
            Id=new Guid(),
            Name="Test2",
            ImageUrl="bilmemne.png",
            IsUsable=false,
            CreatedAt=DateTime.UtcNow.Ticks
        };

        //Arrange
        List<Category> categoryList = new();
        categoryList.Add(category1);
        categoryList.Add(category2);

        var category=new Category();
        _categoryRepository.GetAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(Task.FromResult(category));
        categoryList.Adapt<GetCategoryResponse>();

        var categoryGetByIdResponse= new GetCategoryResponse()
        {
            Id = categoryId,
            Name=category1.Name,
            ImageUrl = category1.ImageUrl,
            IsUsable=category1.IsUsable,
        };

        var query= new GetCategoryById.Query(categoryId);
        var queryHandler= new GetCategoryById.QueryHandler(_categoryRepository);

        //Act
        var result=await queryHandler.Handle(query, CancellationToken.None);

        //Assert
        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.NotNull(result.Data);


    }



    [Fact]
    public async Task CreateCategory_Should_Success_WhenCreated()
    {
        //Arrange

        var category= new Category()
        {
            Id=new Guid(),
            Name="CategoryTest",
            ImageUrl="categorytest.png",
            IsUsable=true
        };
        _categoryRepository.CreateAsync(Arg.Any<Category>()).Returns(Task.FromResult(category));
        category.Adapt<CreatedCategoryResponse>();

        var createdCategoryResponse=new CreatedCategoryResponse()
        {
            Id=category.Id,
            Name=category.Name,
            ImageUrl = category.ImageUrl,
            IsUsable=category.IsUsable
        };

        var request= new CreateCategoryRequest(category.Name,category.ImageUrl,category.IsUsable);
        var command= new CreateCategory.Command(request);
        var commandHandler= new CreateCategory.CommandHandler(_categoryRepository, _unitOfWork);

        //Act
        var result= await commandHandler.Handle(command,CancellationToken.None);

        //Assert
        Assert.NotNull(result.Data);
        Assert.Null(result.Errors);
        Assert.True(result.StatusCode == 201);
        //Assert.Equal(result.Data.Id, createdCategoryResponse.Id);
        //Assert.Equal(result.Data.Name, createdCategoryResponse.Name);
        //Assert.Equal(result.Data.IsUsable, createdCategoryResponse.IsUsable);
        //Assert.Equal(result.Data.ImageUrl, createdCategoryResponse.ImageUrl);
    }


    [Fact]
    public async Task UpdateCategory_Should_Return_UpdateCategoryResponse_WhenSuccess()
    {

        var category= new Category()
        {

            Id = new Guid(),
            CreatedAt = DateTime.UtcNow.Ticks,
            Name= "Test",
            IsUsable = false,
            ImageUrl= null
        };
        var newCategoryValues=new Category
        {
            Name="New Name",
            IsUsable=true,
            ImageUrl="newUrl.jpeg"
        };

        _categoryRepository.GetAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(Task.FromResult(category));
        _categoryRepository.UpdateAsync(Arg.Any<Category>()).Returns(Task.FromResult(category));
        category.Adapt<UpdatedCategoryResponse>();

        var updatedCategory= new UpdatedCategoryResponse()
        {
            Id=category.Id,
            Name=newCategoryValues.Name,
            ImageUrl = newCategoryValues.ImageUrl,
            IsUsable=newCategoryValues.IsUsable,

        };

        var request= new UpdateCategoryRequest(newCategoryValues.Name,newCategoryValues.ImageUrl,newCategoryValues.IsUsable);
        var command= new UpdateCategory.Command(category.Id,request);
        var commandHandler=new UpdateCategory.CommandHandler(_categoryRepository, _unitOfWork);

        //Act
        var result = await commandHandler.Handle(command,CancellationToken.None);

        //Assert
        Assert.Null(result.Errors);
        Assert.True(result.StatusCode == 204);
        Assert.NotNull(result.Data);
        Assert.Equal(result.Data.Id, updatedCategory.Id);
        Assert.Equal(result.Data.Name, updatedCategory.Name);
        Assert.Equal(result.Data.ImageUrl, updatedCategory.ImageUrl);
        Assert.Equal(result.Data.IsUsable, updatedCategory.IsUsable);

    }


    [Fact]
    public async Task DeleteCategory_Should_Success_WhenDeleted()
    {
        var category= new Category()
        {
            Id =new Guid(),
            Name="Rap",
            ImageUrl="ceza.png",
            IsUsable=true,
            CreatedAt=DateTime.UtcNow.Ticks,
            DeletedAt=null
        };
        _categoryRepository.GetAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(Task.FromResult(category));
        _categoryRepository.RemoveAsync(Arg.Any<Category>()).Returns(Task.FromResult(category));

        var command=new DeleteCategory.Command(category.Id);
        var commandHandler=new DeleteCategory.CommandHandler(_categoryRepository,_unitOfWork);
        //Act
        var result= await commandHandler.Handle(command, CancellationToken.None);
        Assert.Null(result.Errors);
        Assert.True(result.StatusCode == 204);
    }

}
