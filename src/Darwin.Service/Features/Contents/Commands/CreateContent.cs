using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Contents;
using Darwin.Model.Response.Contents;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Contents.Commands;

public static class CreateContent
{
    public record Command(CreateContentRequest Model) : ICommand<DarwinResponse<CreatedContentResponse>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedContentResponse>>
    {

        private readonly IGenericRepository<Content> _contentRepositoryAsync;
        private readonly IGenericRepository<Category> _categoryRepositoryAsync;
        private readonly IGenericRepository<Mood> _moodRepositoryAsync;
        private readonly IGenericRepository<AgeRate> _ageRateRepositoryAsync;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IGenericRepository<Content> contentRepositoryAsync,
            IGenericRepository<Category> categoryRepositoryAsync,
            IGenericRepository<Mood> moodRepositoryAsync,
            IGenericRepository<AgeRate> ageRateRepositoryAsync,
            IUnitOfWork unitOfWork)
        {
            _contentRepositoryAsync = contentRepositoryAsync;
            _categoryRepositoryAsync = categoryRepositoryAsync;
            _moodRepositoryAsync = moodRepositoryAsync;
            _ageRateRepositoryAsync = ageRateRepositoryAsync;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<CreatedContentResponse>> Handle(Command request, CancellationToken cancellationToken)
        {

            ////Age Rate

            var ageRate= await _ageRateRepositoryAsync.GetAsync(x=>x.Id==request.Model.AgeRateId);
            if (ageRate is null)
            {
                return DarwinResponse<CreatedContentResponse>.Fail("NotFound", 404);
            }

            List<Mood> moodList=new();
            foreach (var moodId in request.Model.MoodIds)
            {
                Mood existMood = await _moodRepositoryAsync.GetAsync(x => x.Id == moodId);
                if (existMood is not null)
                {
                    moodList.Add(existMood);
                }
            }

            //Categories
            List<Category> categoryList=new();
            foreach (var categoryId in request.Model.CategoryIds)
            {
                Category existCategory = await _categoryRepositoryAsync.GetAsync(x => x.Id == categoryId);
                if (existCategory != null)
                {
                    categoryList.Add(existCategory);
                }
            }

            var content = new Content()
            {
                Name = request.Model.Name,
                Lyrics= request.Model.Lyrics,
                ImageUrl = request.Model.ImageUrl,
                IsUsable = request.Model.IsUsable,
                AgeRate=ageRate,
                Categories =categoryList,
                Moods =moodList
            };

            //SaveOperations
            await _contentRepositoryAsync.CreateAsync(content);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<CreatedContentResponse>.Success(content.Adapt<CreatedContentResponse>(), 201);
        }
    }

    public class CreateContentCommandValidator : AbstractValidator<Command>
    {
        public CreateContentCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
            RuleFor(x => x.Model.AgeRateId).NotNull();
            RuleFor(x => x.Model.MoodIds).NotNull();
            RuleFor(x => x.Model.CategoryIds).NotNull();
        }
    }
}