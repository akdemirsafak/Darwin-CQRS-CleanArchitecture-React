using Darwin.Domain.UnitofWorkCore;
using MediatR;

namespace Darwin.Application.Behaviors;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (IsNotCommand())
        {
            return await next();
        }
        var response =await next();

        await _unitOfWork.CommitAsync(cancellationToken);


        return response;
        // Bu şekilde çalışır fakat tek bir ilişkili veri eklemek istediğimizde ilk tabloya kayıt eklendi ve bu kaydın id sini kullanan bir kayıt başka tabloya kayıt eklenecek.
        //                                                  
        // Fakat burada ilk tbloya kaydettiğimiz kaydın id sini alamayacağız. burada DBContext OnmodelCreateating de UseHilo() kullanmak bir çözümdür.Fakat biz transaction'ı daha kontrollü hale getirelim.

        //using( var transactionScope= new TransactionScope())
        //{
        //    var response = await next();
        //    await _unitOfWork.CommitAsync(cancellationToken);
        //    transactionScope.Complete();
        //    return response;
        //}

    }
    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.EndsWith("Command");
    }
}
