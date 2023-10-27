using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.AgeRates;
using Darwin.Model.Response.AgeRates;
using Darwin.Service.Features.AgeRates.Commands;
using Darwin.Service.Features.AgeRates.Queries;
using Mapster;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Linq.Expressions;

namespace Darwin.UnitTests;

public class AgeRateTests
{

    private readonly IGenericRepository<AgeRate> _ageRateRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AgeRateTests()
    {
        _ageRateRepository = Substitute.For<IGenericRepository<AgeRate>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task GetAgeRates_When_FoundDatas()
    {

        //Arrange
        var ageRateList=new List<AgeRate>();
        ageRateList.Add(new AgeRate()
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            Name = "Genel",
            Rate = 7
        });
        ageRateList.Add(new AgeRate()
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            Name = "Adult",
            Rate = 18
        });


        _ageRateRepository.GetAllAsync().Returns(Task.FromResult(ageRateList));
        var ageRateListResponse=ageRateList.Adapt<List<GetAgeRateResponse>>();

        var query=new GetAgeRates.Query();
        var queryHandler=new GetAgeRates.QueryHandler(_ageRateRepository);


        //Act

        var result=await queryHandler.Handle(query,CancellationToken.None);

        //Assert
        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.NotNull(result.Data);
        Assert.Equivalent(result.Data, ageRateListResponse);


    }

    [Fact]
    public async Task CreateAgeRates_Should_201Created_When_Successfull()
    {
        //Arrange
        var ageRate=new AgeRate
        {
            Id= Guid.NewGuid(),
            IsActive = true,
            Name="Adult",
            Rate= 18
        };

        _ageRateRepository.CreateAsync(Arg.Any<AgeRate>()).Returns(Task.FromResult(ageRate));

        var request= new CreateAgeRateRequest(ageRate.Rate,ageRate.Name,ageRate.IsActive);
        var command= new CreateAgeRate.Command(request);
        var commandHandler= new CreateAgeRate.CommandHandler(_ageRateRepository, _unitOfWork);


        //Act
        var result= await commandHandler.Handle(command,CancellationToken.None);
        //Assert

        Assert.True(result.StatusCode == StatusCodes.Status201Created);
    }

    [Fact]
    public async Task DeleteAgeRate_Should_204DeletedAgeRateResponse_When_Successfull()
    {

        //Arrange

        var ageRateId = Guid.NewGuid();
        var ageRate=new AgeRate
        {
            Id = ageRateId,
            IsActive = true,
            Name="Adult",
            Rate= 18
        };

        _ageRateRepository.GetAsync(Arg.Any<Expression<Func<AgeRate, bool>>>()).Returns(Task.FromResult(ageRate));
        ageRate.IsActive = false;
        _ageRateRepository.UpdateAsync(Arg.Any<AgeRate>()).Returns(Task.FromResult(ageRate));


        var command= new DeleteAgeRate.Command(ageRate.Id);
        var commandHandler= new DeleteAgeRate.CommandHandler(_ageRateRepository, _unitOfWork);


        //Act

        var result= await commandHandler.Handle(command,CancellationToken.None);

        //Assert
        Assert.True(result.StatusCode == StatusCodes.Status204NoContent); //DeleteAgeRate returning DeletedAgeRateResponse
    }

}
