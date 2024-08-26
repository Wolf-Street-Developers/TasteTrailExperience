using Microsoft.AspNetCore.Identity;
using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailExperience.Core.Common.Exceptions;
using TasteTrailExperience.Core.Feedbacks.Dtos;
using TasteTrailExperience.Core.Feedbacks.Repositories;
using TasteTrailExperience.Core.Feedbacks.Services;
using TasteTrailExperience.Core.Filters.Dtos;
using TasteTrailExperience.Core.Filters.Models;
using TasteTrailExperience.Core.Venues.Repositories;
using TasteTrailExperience.Infrastructure.Feedbacks.Factories;

namespace TasteTrailExperience.Infrastructure.Feedbacks.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;

    private readonly IVenueRepository _venueRepository;

    private readonly UserManager<User> _userManager;

    public FeedbackService(IFeedbackRepository feedbackRepository,  IVenueRepository venueRepository,
        UserManager<User> userManager)
    {
        _feedbackRepository = feedbackRepository;
        _userManager = userManager;
        _venueRepository = venueRepository;
    }

    public async Task<FilterResponseDto<FeedbackGetDto>> GetFeedbacksFiltered(FilterParametersDto filterParameters, int venueId)
    {
        if (venueId <= 0)
            throw new ArgumentException($"Invalid Venue ID: {venueId}.");

        var newFilterParameters = new FilterParameters<Feedback>() {
            PageNumber = filterParameters.PageNumber,
            PageSize = filterParameters.PageSize,
            Specification = FeedbackFilterFactory.CreateFilter(filterParameters.Type),
            SearchTerm = null
        };

        var feedbacks = await _feedbackRepository.GetFilteredByIdAsync(newFilterParameters, venueId);
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
                UserId = user.Id,
                VenueId = feedback.VenueId,
            };

            feedbackDtos.Add(feedbackDto);
        }

        var totalFeedbacks = await _feedbackRepository.GetCountBySpecificationIdAsync(newFilterParameters.Specification, venueId);
        var totalPages = (int)Math.Ceiling(totalFeedbacks / (double)filterParameters.PageSize);


        var filterReponse = new FilterResponseDto<FeedbackGetDto>() {
            CurrentPage = filterParameters.PageNumber,
            AmountOfPages = totalPages,
            AmountOfEntities = totalFeedbacks,
            Entities = feedbackDtos
        };

        return filterReponse;
    }

    public async Task<FeedbackGetDto?> GetFeedbackByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

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
            UserId = user.Id,
            VenueId = feedback.VenueId,
        };

        return feedbackDto;
    }

    public async Task<int> GetFeedbacksCountAsync()
    {
        return await _feedbackRepository.GetCountAsync();
    }

    public async Task<int> CreateFeedbackAsync(FeedbackCreateDto feedback, User user)
    {
        var venue = await _venueRepository.GetByIdAsync(feedback.VenueId) ?? 
            throw new ArgumentException($"Venue by ID: {feedback.VenueId} not found.");

        var newFeedback = new Feedback
        {
            Text = feedback.Text,
            Rating = feedback.Rating,
            CreationDate = DateTime.Now.ToUniversalTime(),
            UserId = user.Id,
            VenueId = venue.Id,
        };

        var feedbackId = await _feedbackRepository.CreateAsync(newFeedback);
        await UpdateVenueRatingAsync(newFeedback.VenueId);
        
        return feedbackId;
    }

    public async Task<int?> DeleteFeedbackByIdAsync(int id, User user)
    {
        if (id <= 0)
            throw new ArgumentException($"Invalid ID value: {id}.");

        var feedbackToUpdate = await _feedbackRepository.GetAsNoTrackingAsync(id);

        if (feedbackToUpdate is null)
            return null;

        if (feedbackToUpdate.UserId != user.Id)
            throw new ForbiddenAccessException();

        var feedbackId = await _feedbackRepository.DeleteByIdAsync(id);

        return feedbackId;
    }


    public async Task<int?> PutFeedbackAsync(FeedbackUpdateDto feedback, User user)
    {
        var feedbackToUpdate = await _feedbackRepository.GetAsNoTrackingAsync(feedback.Id);

        if (feedbackToUpdate is null)
            return null;

        if (feedbackToUpdate.UserId != user.Id)
            throw new ForbiddenAccessException();

        var venue = await _venueRepository.GetByIdAsync(feedbackToUpdate.VenueId);

        var updatedFeedback = new Feedback
        {
            Id = feedback.Id,
            Text = feedback.Text,
            Rating = feedback.Rating,
            CreationDate = DateTime.Now.ToUniversalTime(),
            UserId = user.Id
        };

        var feedbackId = await _feedbackRepository.PutAsync(updatedFeedback);
        await UpdateVenueRatingAsync(venue!.Id);

        return feedbackId;
    }

    private async Task UpdateVenueRatingAsync(int venueId) {
        if (venueId <= 0)
            throw new ArgumentException($"Rating Update Error. Invalid ID value: {venueId}.");

        var venue = await _venueRepository.GetByIdAsync(venueId);

        if (venue is null)
            throw new ArgumentNullException(nameof(venueId));

        venue.OverallRating = (float)Math.Round(venue.Feedbacks.Average(f => f.Rating), 2);

        await _venueRepository.PutAsync(venue);
    }
}
