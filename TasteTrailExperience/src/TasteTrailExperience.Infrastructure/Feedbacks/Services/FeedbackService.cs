using Microsoft.AspNetCore.Identity;
using Npgsql.TypeMapping;
using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Feedbacks.Dtos;
using TasteTrailExperience.Core.Feedbacks.Repositories;
using TasteTrailExperience.Core.Feedbacks.Services;

namespace TasteTrailExperience.Infrastructure.Feedbacks.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;

    private readonly UserManager<User> _userManager;

    public FeedbackService(IFeedbackRepository feedbackRepository, UserManager<User> userManager)
    {
        _feedbackRepository = feedbackRepository;
        _userManager = userManager;
    }

    public async Task<List<FeedbackGetDto>> GetFeedbacksByCountAsync(int count)
    {
        var feedbacks = await _feedbackRepository.GetByCountAsync(count);
        var feedbackDtos = new List<FeedbackGetDto>();

        foreach (var feedback in feedbacks)
        {
            var user = await _userManager.FindByIdAsync(feedback.UserId);

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var feedbackDto = new FeedbackGetDto
            {
                Id = feedback.Id,
                Text = feedback.Text,
                Rating = feedback.Rating,
                CreationDate = feedback.CreationDate,
                Username = user.UserName!,
                VenueId = feedback.VenueId,
            };

            feedbackDtos.Add(feedbackDto);
        }

        return feedbackDtos;
    }

    public async Task<FeedbackGetDto?> GetFeedbackByIdAsync(int id)
    {
        var feedback = await _feedbackRepository.GetByIdAsync(id);
        
        if (feedback is null)
            return null;

        var user = await _userManager.FindByIdAsync(feedback.UserId);

        if (user is null)
            throw new ArgumentNullException(nameof(user));

        var feedbackDto = new FeedbackGetDto
        {
            Id = feedback.Id,
            Text = feedback.Text,
            Rating = feedback.Rating,
            CreationDate = feedback.CreationDate,
            Username = user.UserName!,
            VenueId = feedback.VenueId,
        };

        return feedbackDto;
    }

    public async Task<int> CreateFeedbackAsync(FeedbackCreateDto feedback, User user)
    {
        var newFeedback = new Feedback
        {
            Text = feedback.Text,
            Rating = feedback.Rating,
            CreationDate = DateTime.Now.ToUniversalTime(),
            UserId = user.Id,
            VenueId = feedback.VenueId,
        };

        var feedbackId = await _feedbackRepository.CreateAsync(newFeedback);

        return feedbackId;
    }

    public async Task<int?> DeleteFeedbackByIdAsync(int id)
    {
        var feedbackId = await _feedbackRepository.DeleteByIdAsync(id);

        return feedbackId;
    }


    public async Task<int?> UpdateFeedbackAsync(FeedbackUpdateDto feedback, User user)
    {
        var updatedFeedback = new Feedback
        {
            Id = feedback.Id,
            Text = feedback.Text,
            Rating = feedback.Rating,
            CreationDate = DateTime.Now.ToUniversalTime(),
            UserId = user.Id,
            VenueId = feedback.VenueId,
        };

        var feedbackId = await _feedbackRepository.PutAsync(updatedFeedback);

        return feedbackId;
    }
}
