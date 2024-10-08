using TasteTrailData.Core.FeedbackLikes.Models;
using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.FeedbackLikes.Dtos;
using TasteTrailExperience.Core.FeedbackLikes.Repositories;
using TasteTrailExperience.Core.FeedbackLikes.Services;
using TasteTrailExperience.Core.Feedbacks.Repositories;

namespace TasteTrailExperience.Infrastructure.FeedbackLikes.Services;

public class FeedbackLikeService : IFeedbackLikeService
{
    private readonly IFeedbackLikeRepository _feedbackLikeRepository;
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackLikeService(IFeedbackLikeRepository feedbackLikeRepository, IFeedbackRepository feedbackRepository)
    {
        _feedbackLikeRepository = feedbackLikeRepository;
        _feedbackRepository = feedbackRepository;
    }

    public async Task<int> CreateFeedbackLikeAsync(FeedbackLikeCreateDto feedbackLikeCreateDto, User user)
    {
        var feedbackLikeToCreate = new FeedbackLike()
        {
            FeedbackId = feedbackLikeCreateDto.FeedbackId,
            UserId = user.Id,
        };

        var exists = await _feedbackLikeRepository.Exists(feedbackLikeToCreate.FeedbackId, feedbackLikeToCreate.UserId);

        if (exists)
            throw new InvalidOperationException($"You already liked this feedback.");

        var id = await _feedbackLikeRepository.CreateAsync(feedbackLikeToCreate);

        var feedbackId = await _feedbackRepository.IncrementLikesAsync(new Feedback()
        {
            Id = feedbackLikeToCreate.FeedbackId,
            UserId = user.Id,
        });

        if(feedbackId is null)
        {
            await _feedbackLikeRepository.DeleteByIdAsync(id);
        }

        return id;
    }

    public async Task<int?> DeleteFeedbackLikeByIdAsync(int id, User user)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

        var feedbackLikeToDelete = await _feedbackLikeRepository.GetAsNoTrackingAsync(id);

        if (feedbackLikeToDelete is null)
            return null;

        if (feedbackLikeToDelete.UserId != user.Id)
            throw new ForbiddenAccessException();

        var feedbackLikeId = await _feedbackLikeRepository.DeleteByIdAsync(id);
        
        var feedbackId = await _feedbackRepository.DecrementLikesAsync(new Feedback()
        {
            Id = feedbackLikeToDelete.FeedbackId,
            UserId = user.Id,
        });

        if(feedbackId is null)
        {
            await _feedbackLikeRepository.CreateAsync(new FeedbackLike()
            {
                FeedbackId = feedbackLikeToDelete.FeedbackId,
                UserId = user.Id,
            });
        }

        return feedbackLikeId;
    }
}
