using Microsoft.EntityFrameworkCore;
using TasteTrailData.Core.FeedbackLikes.Models;
using TasteTrailData.Infrastructure.Common.Data;
using TasteTrailExperience.Core.FeedbackLikes.Repositories;

namespace TasteTrailExperience.Infrastructure.FeedbackLikes.Repositories;

public class FeedbackLikeEfCoreRepository : IFeedbackLikeRepository
{
    private readonly TasteTrailDbContext _dbContext;

    public FeedbackLikeEfCoreRepository(TasteTrailDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<int> CreateAsync(FeedbackLike feedbackLike)
    {
        ArgumentNullException.ThrowIfNull(feedbackLike);

        var feedback = await _dbContext.Feedbacks.FirstOrDefaultAsync(m => m.Id == feedbackLike.FeedbackId) ?? 
            throw new ArgumentException($"Feedback by ID: {feedbackLike.FeedbackId} not found.");

        var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Id == feedbackLike.UserId) ?? 
            throw new ArgumentException($"User by ID: {feedbackLike.UserId} not found.");

        await _dbContext.FeedbackLikes.AddAsync(feedbackLike);
        await _dbContext.SaveChangesAsync();

        return feedbackLike.Id;
    }

    public async Task<int?> DeleteByIdAsync(int id)
    {
        var feedbackLike = await _dbContext.FeedbackLikes.FindAsync(id);

        if (feedbackLike is null)
            return null;
        
        _dbContext.FeedbackLikes.Remove(feedbackLike);
        await _dbContext.SaveChangesAsync();

        return id;
    }

    public async Task<FeedbackLike?> GetAsNoTrackingAsync(int id)
    {
        return await _dbContext.FeedbackLikes
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
    }
}
