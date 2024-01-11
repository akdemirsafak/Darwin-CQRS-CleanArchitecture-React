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
    public CategoryTests()
    {
        _categoryRepository = Substitute.For<IGenericRepository<Category>>();
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
                CreatedOnUtc=DateTime.UtcNow
            },
            new Category()
            {
                Id=new Guid(),
                Name="Test2",
                ImageUrl="bilmemne.png",
                IsUsable=false,
                CreatedOnUtc=DateTime.UtcNow
            }

        };


        _categoryRepository.GetAllAsync().Returns(Task.FromResult(categoryList));
        var categoryListResponse = categoryList.Adapt<List<GetCategoryResponse>>();

        var query = new GetCategories.Query();
        var queryHandler = new GetCategories.QueryHandler(_categoryRepository);

        //Act
        var result = await queryHandler.Handle(query, CancellationToken.None);

        //Assert
        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.NotNull(result.Data);
        Assert.Equivalent(result.Data, categoryListResponse);

    }



    //[Fact]
    //public async Task GetCategoryById_Should_WhenFound()
    //{

    //    /////-----Arrange-----

    //    //Contents
    //    var category= new Category()
    //    {
    //        Id=Guid.NewGuid(),
    //        Name="Anyone",
    //        IsUsable=true,
    //        ImageUrl="darwinCategory.png"
    //    };


    //    _categoryRepository.GetAsync(x=>x.Id==category.Id).Returns(Task.FromResult(category));
    //    var categoryResponse= category.Adapt<GetCategoryResponse>();

    //    var query= new GetCategoryById.Query(category.Id);
    //    var queryHandler=new GetCategoryById.QueryHandler(_categoryRepository);

    //    /////-----Act-----
    //    var result= await queryHandler.Handle(query,CancellationToken.None);

    //    /////-----Assert-----

    //    Assert.True(result.StatusCode == StatusCodes.Status200OK);
    //    Assert.NotNull(result.Data);
    //    Assert.Equivalent(categoryResponse, result.Data);


    //}



    [Fact]
    public async Task CreateCategory_Should_SuccessWith201StatusCode_WhenCreated()
    {
        //Arrange

        var category = new Category()
        {
            Name = "CategoryTest",
            ImageUrl = "categorytest.png",
            IsUsable = true
        };
        _categoryRepository.CreateAsync(Arg.Any<Category>()).Returns(Task.FromResult(category));
        category.Adapt<CreatedCategoryResponse>();
        var createdCategoryResponse = new CreatedCategoryResponse()
        {
            Id = category.Id,
            Name = category.Name,
            ImageUrl = category.ImageUrl,
            IsUsable = category.IsUsable
        };

        var request = new CreateCategoryRequest(category.Name, category.ImageUrl, category.IsUsable);
        var command = new CreateCategory.Command(request);
        var commandHandler = new CreateCategory.CommandHandler(_categoryRepository);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result.Data);
        Assert.Null(result.Errors);
        Assert.True(result.StatusCode == StatusCodes.Status201Created);
        Assert.Equivalent(result.Data, createdCategoryResponse);

    }


    [Fact]
    public async Task UpdateCategory_Should_Return_UpdateCategoryResponse_WhenSuccess()
    {
        var categoryId = Guid.NewGuid();
        var category = new Category()
        {
            Id = categoryId,
            CreatedOnUtc=DateTime.UtcNow,
            Name = "Test",
            IsUsable = false,
            ImageUrl = null
        };
        var newCategoryValues = new Category
        {
            Name = "New Name",
            IsUsable = true,
            ImageUrl = "newUrl.jpeg"
        };

        _categoryRepository.GetAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(Task.FromResult(category));
        _categoryRepository.UpdateAsync(Arg.Any<Category>()).Returns(Task.FromResult(newCategoryValues));
        var updatedCategoryResponse = new UpdatedCategoryResponse
        {
            Id = categoryId,
            ImageUrl = newCategoryValues.ImageUrl,
            IsUsable = newCategoryValues.IsUsable,
            Name = newCategoryValues.Name
        };



        var request = new UpdateCategoryRequest(newCategoryValues.Name, newCategoryValues.ImageUrl, newCategoryValues.IsUsable);
        var command = new UpdateCategory.Command(category.Id, request);
        var commandHandler = new UpdateCategory.CommandHandler(_categoryRepository);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Null(result.Errors);
        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.NotNull(result.Data);
        Assert.Equivalent(updatedCategoryResponse, result.Data);

    }


    [Fact]
    public async Task DeleteCategory_Should_SuccessWithNoContent204StatusCode_WhenDeleted()
    {
        var category = new Category()
        {
            Id = new Guid(),
            Name = "Rap",
            ImageUrl = "ceza.png",
            IsUsable = true,
            CreatedOnUtc = DateTime.UtcNow,
        };
        _categoryRepository.GetAsync(Arg.Any<Expression<Func<Category, bool>>>()).Returns(Task.FromResult(category));
        _categoryRepository.RemoveAsync(Arg.Any<Category>()).Returns(Task.FromResult(category));

        var command = new DeleteCategory.Command(category.Id);
        var commandHandler = new DeleteCategory.CommandHandler(_categoryRepository);
        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);
        Assert.Null(result.Errors);
        Assert.True(result.StatusCode == StatusCodes.Status204NoContent);
    }

}
