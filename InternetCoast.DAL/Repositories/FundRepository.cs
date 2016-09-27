using System.Collections.Generic;
using System.Linq;
using InternetCoast.Infrastructure.Data.EF;
using InternetCoast.Model.Context;
using InternetCoast.Model.Entities;

namespace InternetCoast.DAL.Repositories
{
    public class FundRepository : GenericRepository<Fund>
    {
        private readonly AppDbContext _context;

        public FundRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IQueryable<Fund> GetByFilter(string filter, int? take, int? skip = 0)
        {
            IQueryable<Fund> query =
                _context.Fund.OrderByDescending(e => e.DateCreated);

            if (!string.IsNullOrEmpty(filter))
                query = query.Where(e => e.FundTopic.Contains(filter));

            if (skip.HasValue && skip.Value > 0)
                query = query.Skip(skip.Value);

            if (take.HasValue && take.Value > 0)
                query = query.Take(take.Value);

            return query;
        }

        //public FilteredFunds GetFilteredApplications(string filter, int? take, int? skip)
        //{
        //    var result = new FilteredFunds();

        //    if (string.IsNullOrEmpty(filter) && !skip.HasValue)
        //    {
        //        result.Applications = GetByFilter(null, take, null).ToList();
        //        result.TotalApplications = Count();
        //    }
        //    else
        //    {
        //        result.Applications = GetByFilter(filter, take, skip).ToList();
        //        result.TotalApplications = GetByFilter(filter, null, null).Count();
        //    }

        //    return result;
        //}

        //public FilteredFunds GetFilteredApplicationsForReviewers(string filter, int? take, int? skip)
        //{
        //    var result = new FilteredFunds();

        //    if (string.IsNullOrEmpty(filter) && !skip.HasValue)
        //    {
        //        result.Applications = GetByFilterForReviewers(null, take, null).ToList();
        //        result.TotalApplications = Count();
        //    }
        //    else
        //    {
        //        result.Applications = GetByFilterForReviewers(filter, take, skip).ToList();
        //        result.TotalApplications = GetByFilterForReviewers(filter, null, null).Count();
        //    }

        //    return result;
        //}

        //public IQueryable<MediaTypeNames.Application> GetByFilterForReviewers(string filter, int? take, int? skip = 0)
        //{
        //    IQueryable<MediaTypeNames.Application> query =
        //        _context.Application.OrderByDescending(e => e.DateCreated);

        //    query = query.Where(a => a.Statuses.FirstOrDefault(s => s.Active).Status.StatusCode.Equals("APPUNR"));

        //    if (!string.IsNullOrEmpty(filter))
        //        query = query.Where(e => e.Applicant.User.FirstName.Contains(filter) ||
        //                                 e.Applicant.User.LastName.Contains(filter) ||
        //                                 e.Applicant.User.Email.Contains(filter) ||
        //                                 e.Applicant.User.MiddleName.Contains(filter));

        //    if (skip.HasValue && skip.Value > 0)
        //        query = query.Skip(skip.Value);

        //    if (take.HasValue && take.Value > 0)
        //        query = query.Take(take.Value);

        //    return query;
        //}

        //public bool ChangeApplicationStatus(int applicationId, string statusCode)
        //{
        //    var status = _context.Status.Single(s => s.Active && s.StatusCode.Equals(statusCode));
        //    if (status == null)
        //        return false;

        //    var currentStaus = _context.ApplicationStatus.Single(s => s.Active && s.ApplicationId.Equals(applicationId));

        //    if (currentStaus != null)
        //        currentStaus.Active = false;

        //    var newStatus = new ApplicationStatus { ApplicationId = applicationId, Status = status };

        //    _context.Entry(newStatus).State = EntityState.Added;

        //    return true;
        //}

        //public bool ApplicationReviewerDecision(int applicationId, string decisionCode, string comments, int reviewerId)
        //{
        //    var decision = _context.ReviewerDecision.SingleOrDefault(s => s.Active && s.DecisionCode.Equals(decisionCode));
        //    if (decision == null) return false;

        //    var reviewer = _context.User.Any(u => u.UserId.Equals(reviewerId));
        //    if (!reviewer) return false;

        //    var currentDecision =
        //        _context.ApplicationReviewerDecision.SingleOrDefault(
        //            d => d.ApplicationId.Equals(applicationId) && d.CreatedBy.Equals(reviewerId));

        //    if (currentDecision == null)
        //    {
        //        var newDecision = new ApplicationReviewerDecision()
        //        {
        //            ApplicationId = applicationId,
        //            ReviewerDecisionId = decision.DecisionId,
        //            Comment = comments
        //        };

        //        _context.Entry(newDecision).State = EntityState.Added;
        //    }
        //    else
        //    {
        //        currentDecision.ReviewerDecision = _context.ReviewerDecision.Single(d => d.DecisionCode.Equals(decisionCode));
        //        currentDecision.Comment = comments;
        //        _context.Entry(currentDecision).State = EntityState.Modified;
        //    }

        //    return true;
        //}
    }

    public class FilteredFunds
    {
        public int TotalFunds { get; set; }

        public List<Fund> Funds { get; set; }
    }
}
