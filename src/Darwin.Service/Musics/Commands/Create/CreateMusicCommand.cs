using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Infrastructure;
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
        private readonly DarwinDbContext _dbContext;

        public Handler(DarwinDbContext dbContext)
        {
            _dbContext = dbContext;
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
                Mood existMood = await _dbContext.Moods.FindAsync(moodId);
                if (existMood != null)
                {
                    music.Moods.Add(existMood);
                }
            }
            foreach (var categoryId in request.Model.CategoryIds)
            {
                Category existCategory = await _dbContext.Categories.FindAsync(categoryId);
                if (existCategory != null)
                {
                    music.Categories.Add(existCategory);
                }
            }
            await _dbContext.AddAsync(music);
            await _dbContext.SaveChangesAsync();
            return DarwinResponse<CreatedMusicResponse>.Success(music.Adapt<CreatedMusicResponse>(), 201);
        }
    }
}
