using Microsoft.EntityFrameworkCore;
using TasteTrailData.Core.Feedbacks.Models;
using TasteTrailData.Infrastructure.Common.Data;
using TasteTrailExperience.Core.Feedbacks.Repositories;
using TasteTrailExperience.Core.Filters;

namespace TasteTrailExperience.Infrastructure.Feedbacks.Repositories;

public class FeedbackEfCoreRepository : IFeedbackRepository
{
    private readonly TasteTrailDbContext _dbContext;

    public FeedbackEfCoreRepository(TasteTrailDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<Feedback>> GetFilteredByIdAsync(int venueId, IFilterSpecification<Feedback> specification, 
        int pageNumber, int pageSize)
    {
        IQueryable<Feedback> query = _dbContext.Set<Feedback>();

        query = query.Where(f => f.VenueId == venueId); // Getting feedbacks by VenueId
        query = specification.Apply(query); // Adding Filter
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize); // Applying pagination

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
}
