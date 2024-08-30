using Microsoft.EntityFrameworkCore;
using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailData.Core.Filters.Specifications;
using TasteTrailData.Infrastructure.Common.Data;
using TasteTrailExperience.Core.Feedbacks.Repositories;

namespace TasteTrailExperience.Infrastructure.Feedbacks.Repositories;

public class FeedbackEfCoreRepository : IFeedbackRepository
{
    private readonly TasteTrailDbContext _dbContext;

    public FeedbackEfCoreRepository(TasteTrailDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<Feedback>> GetFilteredByIdAsync(FilterParameters<Feedback> parameters, int venueId)
    {
        IQueryable<Feedback> query = _dbContext.Set<Feedback>();

        query = query.Where(f => f.VenueId == venueId); // Getting feedbacks by VenueId

        if (parameters.Specification is not null)
            query = parameters.Specification.Apply(query); // Adding Filter

        query = query.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize); // Applying pagination

        return await query.ToListAsync();
    }

    public async Task<Feedback?> GetByIdAsync(int id)
    {
        return await _dbContext.Feedbacks
            .FirstOrDefaultAsync(f => f.Id == id);
    }
    
    public async Task<int> GetCountAsync()
    {
        return await _dbContext.Feedbacks.CountAsync();
    }

    public async Task<int> GetCountFilteredIdAsync(FilterParameters<Feedback>? parameters, int venueId)
    {
        var query = _dbContext.Feedbacks.AsQueryable();
        query = query.Where(f => f.VenueId == venueId);

        if (parameters is null)
            return await query.CountAsync();

        if (parameters.Specification != null)
            query = parameters.Specification.Apply(query);

        return await query.CountAsync();
    }

    public async Task<decimal> GetAverageRatingAsync(int venueId) {
        return await _dbContext.Feedbacks
            .Where(f => f.VenueId == venueId)
            .AverageAsync(f => f.Rating);
    }

    public async Task<int> CreateAsync(Feedback feedback)
    {
        ArgumentNullException.ThrowIfNull(feedback);

        await _dbContext.Feedbacks.AddAsync(feedback);
        await _dbContext.SaveChangesAsync();

        return feedback.Id;
    }

    public async Task<int?> DeleteByIdAsync(int id)
    {
        var feedback = await _dbContext.Feedbacks.FindAsync(id);

        if (feedback is null)
            return null;
        
        _dbContext.Feedbacks.Remove(feedback);
        await _dbContext.SaveChangesAsync();

        return id;
    }

    public async Task<int?> PutAsync(Feedback feedback)
    {
        var feedbackToUpdate = await _dbContext.Feedbacks
            .FirstOrDefaultAsync(f => f.Id == feedback.Id);

        if (feedbackToUpdate == null)
            return null;

        feedbackToUpdate.Text = feedback.Text;
        feedbackToUpdate.Rating = feedback.Rating;
        feedbackToUpdate.CreationDate = feedback.CreationDate;
        feedbackToUpdate.UserId = feedback.UserId;

        await _dbContext.SaveChangesAsync();

        return feedback.Id;
    }

    public async Task<Feedback?> GetAsNoTrackingAsync(int id)
    {
        return await _dbContext.Feedbacks
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<int?> IncrementLikesAsync(Feedback feedback)
    {
        var feedbackToUpdate = await _dbContext.Feedbacks
            .FirstOrDefaultAsync(mi => mi.Id == feedback.Id);

        if (feedbackToUpdate is null)
            return null;

        
        feedbackToUpdate.Likes++;

        await _dbContext.SaveChangesAsync();

        return feedback.Id;
    }

    public async Task<int?> DecrementLikesAsync(Feedback feedback)
    {
        var feedbackToUpdate = await _dbContext.Feedbacks
            .FirstOrDefaultAsync(mi => mi.Id == feedback.Id);

        if (feedbackToUpdate is null)
            return null;

        
        feedbackToUpdate.Likes--;

        await _dbContext.SaveChangesAsync();

        return feedback.Id;
    }

}
