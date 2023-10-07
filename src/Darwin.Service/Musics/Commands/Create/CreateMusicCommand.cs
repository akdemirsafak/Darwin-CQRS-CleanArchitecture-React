using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Core.RepositoryCore;
using Darwin.Core.UnitofWorkCore;
using Darwin.Model.Request.Musics;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;
using Mapster;

namespace Darwin.Service.Musics.Commands.Create;

public class CreateMusicCommand : ICommand<DarwinResponse<CreatedMusicResponse>>
{
    public CreateMusicRequest Model { get; }

    public CreateMusicCommand(CreateMusicRequest model)
    {
        Model = model;
    }
    public class Handler : ICommandHandler<CreateMusicCommand, DarwinResponse<CreatedMusicResponse>>
    {

        private readonly IGenericRepository<Music> _musicRepositoryAsync;
        private readonly IGenericRepository<Category> _categoryRepositoryAsync;
        private readonly IGenericRepository<Mood> _moodRepositoryAsync;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IGenericRepository<Music> musicRepositoryAsync, IGenericRepository<Category> categoryRepositoryAsync, IGenericRepository<Mood> moodRepositoryAsync, IUnitOfWork unitOfWork)
        {
            _musicRepositoryAsync = musicRepositoryAsync;
            _categoryRepositoryAsync = categoryRepositoryAsync;
            _moodRepositoryAsync = moodRepositoryAsync;
            _unitOfWork = unitOfWork;
        }

        public async Task<DarwinResponse<CreatedMusicResponse>> Handle(CreateMusicCommand request, CancellationToken cancellationToken)
        {
            Music music = new()
            {
                Name = request.Model.Name,
                ImageUrl = request.Model.ImageUrl,
                IsUsable = request.Model.IsUsable,
                Moods=new List<Mood>(),
                Categories=new List<Category>(),
            };



            foreach (var moodId in request.Model.MoodIds)
            {
                Mood existMood = await _moodRepositoryAsync.GetAsync(x=>x.Id==moodId);
                if (existMood != null)
                {
                    music.Moods.Add(existMood);
                }
            }
            foreach (var categoryId in request.Model.CategoryIds)
            {
                Category existCategory = await _categoryRepositoryAsync.GetAsync(x=>x.Id==categoryId);
                if (existCategory != null)
                {
                    music.Categories.Add(existCategory);
                }
            }
            await _musicRepositoryAsync.CreateAsync(music);
            await _unitOfWork.CommitAsync();
            return DarwinResponse<CreatedMusicResponse>.Success(music.Adapt<CreatedMusicResponse>(), 201);
        }
    }
}
