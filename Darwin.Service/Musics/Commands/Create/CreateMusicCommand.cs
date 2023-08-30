using AutoMapper;
using Darwin.Core.BaseDto;
using Darwin.Core.Entities;
using Darwin.Infrastructure;
using Darwin.Model.Request.Musics;
using Darwin.Model.Response.Musics;
using Darwin.Service.Common;

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
        private readonly IMapper _mapper;

        public Handler(DarwinDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DarwinResponse<CreatedMusicResponse>> Handle(CreateMusicCommand request, CancellationToken cancellationToken)
        {
            Music music = new()
            {
                Name = request.Model.Name,
                ImageUrl = request.Model.ImageUrl,
                IsUsable = request.Model.IsUsable,
                Categories = new List<MusicCategory>(),
                Moods = new List<MusicMood>(),
            };

            foreach (var moodId in request.Model.MoodIds)
            {
                Mood existMood = await _dbContext.Moods.FindAsync(moodId);
                if (existMood != null)
                {
                    music.Moods.Add(new MusicMood() { Mood = existMood });
                }
            }
            foreach (var categoryId in request.Model.CategoryIds)
            {
                Category existCategory = await _dbContext.Categories.FindAsync(categoryId);
                if (existCategory != null)
                {
                    music.Categories.Add(new MusicCategory() { Category = existCategory });
                }
            }
            await _dbContext.AddAsync(music);
            await _dbContext.SaveChangesAsync();
            return DarwinResponse<CreatedMusicResponse>.Success(_mapper.Map<CreatedMusicResponse>(music), 201);
        }
    }
}
