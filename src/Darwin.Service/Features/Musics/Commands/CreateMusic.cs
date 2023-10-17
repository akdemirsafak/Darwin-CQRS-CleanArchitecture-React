using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Musics;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using FluentValidation;
using Mapster;

namespace Darwin.Service.Features.Musics.Commands;

public static class CreateMusic
{
    public record Command(CreateMusicRequest Model) : ICommand<DarwinResponse<CreatedMusicResponse>>;
    public class CommandHandler : ICommandHandler<Command, DarwinResponse<CreatedMusicResponse>>
    {

        private readonly IGenericRepository<Music> _musicRepositoryAsync;
        private readonly IGenericRepository<Category> _categoryRepositoryAsync;
        private readonly IGenericRepository<Mood> _moodRepositoryAsync;
        private readonly IGenericRepository<ContentAgeRate> _contentAgeRateRepositoryAsync;
        private readonly IUnitOfWork _unitOfWork;



        public CommandHandler(IGenericRepository<Music> musicRepositoryAsync,
            IGenericRepository<Category> categoryRepositoryAsync,
            IGenericRepository<Mood> moodRepositoryAsync,
            IGenericRepository<ContentAgeRate> contentAgeRateRepositoryAsync,
            IUnitOfWork unitOfWork)

        {
            _musicRepositoryAsync = musicRepositoryAsync;
            _categoryRepositoryAsync = categoryRepositoryAsync;
            _moodRepositoryAsync = moodRepositoryAsync;
            _unitOfWork = unitOfWork;
            _contentAgeRateRepositoryAsync = contentAgeRateRepositoryAsync;
        }

        public async Task<DarwinResponse<CreatedMusicResponse>> Handle(Command request, CancellationToken cancellationToken)
        {

            ////Age Rate

            var contentAgeRate=await _contentAgeRateRepositoryAsync.GetAsync(x=>x.Id==request.Model.ContentAgeRateId);
            if (contentAgeRate is null)
            {
                return DarwinResponse<CreatedMusicResponse>.Fail("Yaş sınırlaması belirlemelisiniz.");
            }


            //Moods

            HashSet<Mood> moodList=new();
            foreach (var moodId in request.Model.MoodIds)
            {
                Mood existMood = await _moodRepositoryAsync.GetAsync(x => x.Id == moodId);
                if (existMood is not null)
                {
                    moodList.Add(existMood);
                }
            }

            //Categories
            HashSet<Category> categoryList=new();
            foreach (var categoryId in request.Model.CategoryIds)
            {
                Category existCategory = await _categoryRepositoryAsync.GetAsync(x => x.Id == categoryId);
                if (existCategory != null)
                {
                    categoryList.Add(existCategory);
                }
            }

            var music = new Music()
            {
                Name = request.Model.Name,
                ImageUrl = request.Model.ImageUrl,
                IsUsable = request.Model.IsUsable,
                AgeRate=contentAgeRate,
                Categories =categoryList,
                Moods =moodList
            };

            //SaveOperations
            await _musicRepositoryAsync.CreateAsync(music);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<CreatedMusicResponse>.Success(music.Adapt<CreatedMusicResponse>(), 201);
        }
    }

    public class CreateMusicCommandValidator : AbstractValidator<Command>
    {
        public CreateMusicCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().NotNull().Length(3, 64);
            RuleFor(x => x.Model.MoodIds).NotNull();
            RuleFor(x => x.Model.CategoryIds).NotNull();
            
        }
    }
}